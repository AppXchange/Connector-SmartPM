using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;
using System.Threading.Tasks;

namespace Connector.Projects.v1.Project;

public class ProjectDataReader : TypedAsyncDataReaderBase<ProjectDataObject>
{
    private readonly ILogger<ProjectDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ProjectDataReader(
        ILogger<ProjectDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ProjectDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ProjectDataObject? project;
        
        try
        {
            var response = await _apiClient.GetProjectByIdAsync(_projectId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                _logger.LogError("Failed to retrieve project with ID {ProjectId}. Status code: {StatusCode}", _projectId, response.StatusCode);
                throw new Exception($"Failed to retrieve project. API StatusCode: {response.StatusCode}");
            }

            project = response.GetData();
            if (project == null)
            {
                _logger.LogWarning("No project found with ID {ProjectId}", _projectId);
                yield break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving project with ID {ProjectId}", _projectId);
            throw;
        }

        yield return project;
    }
}