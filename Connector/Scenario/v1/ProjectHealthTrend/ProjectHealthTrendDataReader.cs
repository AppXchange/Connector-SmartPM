using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Scenario.v1.ProjectHealthTrend;

public class ProjectHealthTrendDataReader : TypedAsyncDataReaderBase<ProjectHealthTrendDataObject>
{
    private readonly ILogger<ProjectHealthTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public ProjectHealthTrendDataReader(
        ILogger<ProjectHealthTrendDataReader> logger,
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

    public override async IAsyncEnumerable<ProjectHealthTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ProjectHealthTrendDataObject>? trendData = null;

        try
        {
            var response = await _apiClient.GetProjectHealthTrendAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project health trend. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            trendData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching project health trend for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching project health trend for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (trendData == null || trendData.Count == 0)
        {
            _logger.LogInformation("No project health trend data found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        foreach (var dataPoint in trendData)
        {
            yield return dataPoint;
        }
    }
}