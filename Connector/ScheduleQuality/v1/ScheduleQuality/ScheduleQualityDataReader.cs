using Connector.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using ESR.Hosting.CacheWriter;

namespace Connector.ScheduleQuality.v1.ScheduleQuality;

public class ScheduleQualityDataReader : TypedAsyncDataReaderBase<ScheduleQualityDataObject>
{
    private readonly ILogger<ScheduleQualityDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _importLogId;
    private readonly string? _qualityProfileId;

    public ScheduleQualityDataReader(
        ILogger<ScheduleQualityDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string? importLogId = null,
        string? qualityProfileId = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _importLogId = importLogId;
        _qualityProfileId = qualityProfileId;
    }

    public override async IAsyncEnumerable<ScheduleQualityDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ScheduleQualityDataObject? scheduleQuality = null;

        try
        {
            var response = await _apiClient.GetScheduleQualityAsync(
                _projectId,
                _scenarioId,
                _importLogId,
                _qualityProfileId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve schedule quality. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            scheduleQuality = response.GetData();
            if (scheduleQuality == null)
            {
                throw new Exception("Schedule quality response data was null");
            }

            // Set the project and scenario IDs from the request
            scheduleQuality = new ScheduleQualityDataObject
            {
                ProjectId = _projectId,
                ScenarioId = _scenarioId,
                Metrics = scheduleQuality.Metrics,
                Grade = scheduleQuality.Grade,
                QualityProfileId = scheduleQuality.QualityProfileId
            };
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error retrieving schedule quality for Project {ProjectId}, Scenario {ScenarioId}", _projectId, _scenarioId);
            throw;
        }

        yield return scheduleQuality;
    }
}