using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Scenario.v1.ProjectHealth;

public class ProjectHealthDataReader : TypedAsyncDataReaderBase<ProjectHealthDataObject>
{
    private readonly ILogger<ProjectHealthDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public ProjectHealthDataReader(
        ILogger<ProjectHealthDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string? dataDate = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _dataDate = dataDate;
    }

    public override async IAsyncEnumerable<ProjectHealthDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ProjectHealthDataObject? healthData = null;
        
        try
        {
            var response = await _apiClient.GetProjectHealthAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project health. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            healthData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching project health for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching project health for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (healthData == null)
        {
            _logger.LogInformation("No project health data found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        yield return healthData;
    }
}