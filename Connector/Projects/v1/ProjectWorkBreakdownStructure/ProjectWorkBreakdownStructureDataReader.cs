using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Projects.v1.ProjectWorkBreakdownStructure;

public class ProjectWorkBreakdownStructureDataReader : TypedAsyncDataReaderBase<ProjectWorkBreakdownStructureDataObject>
{
    private readonly ILogger<ProjectWorkBreakdownStructureDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ProjectWorkBreakdownStructureDataReader(
        ILogger<ProjectWorkBreakdownStructureDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ProjectWorkBreakdownStructureDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ProjectWorkBreakdownStructureDataObject>? wbsElements = null;

        try
        {
            var response = await _apiClient.GetProjectWorkBreakdownStructureAsync(_projectId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project WBS. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            wbsElements = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching project WBS for project {ProjectId}", _projectId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching project WBS for project {ProjectId}", _projectId);
            throw;
        }

        if (wbsElements == null || wbsElements.Count == 0)
        {
            _logger.LogInformation("No WBS elements found for project {ProjectId}", _projectId);
            yield break;
        }

        foreach (var element in wbsElements)
        {
            yield return element;
        }
    }
}