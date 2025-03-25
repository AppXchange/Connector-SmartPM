using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Scenario.v1.SchedulePerformanceIndexTrend;

public class SchedulePerformanceIndexTrendDataReader : TypedAsyncDataReaderBase<SchedulePerformanceIndexTrendDataObject>
{
    private readonly ILogger<SchedulePerformanceIndexTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public SchedulePerformanceIndexTrendDataReader(
        ILogger<SchedulePerformanceIndexTrendDataReader> logger,
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

    public override async IAsyncEnumerable<SchedulePerformanceIndexTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<SchedulePerformanceIndexTrendDataObject>? trendData = null;

        try
        {
            var response = await _apiClient.GetSchedulePerformanceIndexTrendAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve schedule performance index trend. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            trendData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving schedule performance index trend data");
            throw;
        }

        if (trendData == null || !trendData.Any())
        {
            _logger.LogInformation("No schedule performance index trend data found");
            yield break;
        }

        foreach (var dataPoint in trendData)
        {
            yield return dataPoint;
        }
    }
}