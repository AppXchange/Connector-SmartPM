using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Delay.v1.DelayTable;

public class DelayTableDataReader : TypedAsyncDataReaderBase<DelayTableDataObject>
{
    private readonly ILogger<DelayTableDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;

    public DelayTableDataReader(
        ILogger<DelayTableDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
    }

    public override async IAsyncEnumerable<DelayTableDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<DelayTableDataObject>? delayTables = null;

        try
        {
            var response = await _apiClient.GetDelayTableAsync(
                _projectId,
                _scenarioId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve delay table. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            delayTables = response.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving delay table for Project {ProjectId} and Scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (delayTables != null)
        {
            foreach (var delayTable in delayTables)
            {
                var enrichedDelayTable = new DelayTableDataObject
                {
                    ProjectId = _projectId,
                    ScenarioId = _scenarioId,
                    Period = delayTable.Period,
                    ScheduleName = delayTable.ScheduleName,
                    DataDate = delayTable.DataDate,
                    EndDate = delayTable.EndDate,
                    EndDateVariance = delayTable.EndDateVariance,
                    CriticalPathDelay = delayTable.CriticalPathDelay,
                    CriticalPathRecovery = delayTable.CriticalPathRecovery,
                    DelayRecovery = delayTable.DelayRecovery,
                    FilterId = delayTable.FilterId,
                    Delays = delayTable.Delays,
                    Recoveries = delayTable.Recoveries
                };
                yield return enrichedDelayTable;
            }
        }
    }
}