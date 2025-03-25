using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Text.Json.Serialization;

namespace Connector.Reporting.v1.MonthlyDistribution;

public class VelocityResponse
{
    [JsonPropertyName("velocityList")]
    public List<MonthlyDistributionDataObject> VelocityList { get; set; } = new();
}

public class MonthlyDistributionDataReader : TypedAsyncDataReaderBase<MonthlyDistributionDataObject>
{
    private readonly ILogger<MonthlyDistributionDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public MonthlyDistributionDataReader(
        ILogger<MonthlyDistributionDataReader> logger,
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

    public override async IAsyncEnumerable<MonthlyDistributionDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        VelocityResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetMonthlyDistributionAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve monthly distribution data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving monthly distribution data for Project {ProjectId}, Scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (response?.VelocityList != null)
        {
            foreach (var velocity in response.VelocityList)
            {
                var enrichedVelocity = new MonthlyDistributionDataObject
                {
                    ProjectId = _projectId,
                    ScenarioId = _scenarioId,
                    Date = velocity.Date,
                    BaselineStarts = velocity.BaselineStarts,
                    BaselineFinishes = velocity.BaselineFinishes,
                    CurrentStarts = velocity.CurrentStarts,
                    CurrentFinishes = velocity.CurrentFinishes
                };
                yield return enrichedVelocity;
            }
        }
    }
}