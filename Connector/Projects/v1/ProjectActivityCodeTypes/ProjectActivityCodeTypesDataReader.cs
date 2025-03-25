using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Projects.v1.ProjectActivityCodeTypes;

public class ProjectActivityCodeTypesDataReader : TypedAsyncDataReaderBase<ProjectActivityCodeTypesDataObject>
{
    private readonly ILogger<ProjectActivityCodeTypesDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ProjectActivityCodeTypesDataReader(
        ILogger<ProjectActivityCodeTypesDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ProjectActivityCodeTypesDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ProjectActivityCodeTypesDataObject>? activityCodeTypes = null;

        try
        {
            var response = await _apiClient.GetProjectActivityCodeTypesAsync(_projectId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project activity code types. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            activityCodeTypes = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching project activity code types for project {ProjectId}", _projectId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching project activity code types for project {ProjectId}", _projectId);
            throw;
        }

        if (activityCodeTypes == null || activityCodeTypes.Count == 0)
        {
            _logger.LogInformation("No activity code types found for project {ProjectId}", _projectId);
            yield break;
        }

        foreach (var activityCodeType in activityCodeTypes)
        {
            yield return activityCodeType;
        }
    }
}