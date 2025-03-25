using Connector.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using ESR.Hosting.CacheWriter;

namespace Connector.ScheduleQuality.v1.ScheduleQualityMetricDetails;

public class ScheduleQualityMetricDetailsDataReader : TypedAsyncDataReaderBase<ScheduleQualityMetricDetailsDataObject>
{
    private readonly ILogger<ScheduleQualityMetricDetailsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string _metric;
    private readonly string? _qualityProfileId;

    public ScheduleQualityMetricDetailsDataReader(
        ILogger<ScheduleQualityMetricDetailsDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string metric,
        string? qualityProfileId = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _metric = metric;
        _qualityProfileId = qualityProfileId;
    }

    public override async IAsyncEnumerable<ScheduleQualityMetricDetailsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ScheduleQualityMetricDetailsDataObject>? metricDetails = null;

        try
        {
            var response = await _apiClient.GetScheduleQualityMetricDetailsAsync(
                _projectId,
                _scenarioId,
                _metric,
                _qualityProfileId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve schedule quality metric details. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            metricDetails = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error retrieving schedule quality metric details for Project {ProjectId}, Scenario {ScenarioId}, Metric {Metric}",
                _projectId, _scenarioId, _metric);
            throw;
        }

        if (metricDetails != null)
        {
            foreach (var detail in metricDetails)
            {
                yield return detail;
            }
        }
    }
}