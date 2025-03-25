using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Connector.Reporting.v1.ShouldStartFinishTrend;

public class HitRatesResponse
{
    [JsonPropertyName("hitRates")]
    public List<ShouldStartFinishTrendDataObject> HitRates { get; set; } = new();
}

public class ShouldStartFinishTrendDataReader : TypedAsyncDataReaderBase<ShouldStartFinishTrendDataObject>
{
    private readonly ILogger<ShouldStartFinishTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;

    public ShouldStartFinishTrendDataReader(
        ILogger<ShouldStartFinishTrendDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
    }

    public override async IAsyncEnumerable<ShouldStartFinishTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        HitRatesResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetShouldStartFinishTrendAsync(
                _projectId,
                _scenarioId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve should start/finish trend data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving should start/finish trend data for Project {ProjectId}, Scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (response?.HitRates != null)
        {
            foreach (var hitRate in response.HitRates)
            {
                // Ensure the key fields are set
                var enrichedHitRate = new ShouldStartFinishTrendDataObject
                {
                    ProjectId = _projectId,
                    ScenarioId = _scenarioId,
                    DataDate = hitRate.DataDate,
                    Total = hitRate.Total,
                    StartedLate = hitRate.StartedLate,
                    FinishedLate = hitRate.FinishedLate,
                    DidNotStart = hitRate.DidNotStart,
                    DidNotFinish = hitRate.DidNotFinish,
                    StartedOnTime = hitRate.StartedOnTime,
                    FinishedOnTime = hitRate.FinishedOnTime,
                    StartedOnTimeHitRate = hitRate.StartedOnTimeHitRate,
                    FinishedOnTimeHitRate = hitRate.FinishedOnTimeHitRate,
                    TotalOnTimeHitRate = hitRate.TotalOnTimeHitRate
                };
                yield return enrichedHitRate;
            }
        }
    }
}