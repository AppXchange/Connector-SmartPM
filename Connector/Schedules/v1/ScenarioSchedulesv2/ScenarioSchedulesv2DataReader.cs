using Connector.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using System.Runtime.CompilerServices;
using ESR.Hosting.CacheWriter;

namespace Connector.Schedules.v1.ScenarioSchedulesv2;

public class ScenarioSchedulesv2DataReader : TypedAsyncDataReaderBase<ScenarioSchedulesv2DataObject>
{
    private readonly ILogger<ScenarioSchedulesv2DataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _asOf;

    public ScenarioSchedulesv2DataReader(
        ILogger<ScenarioSchedulesv2DataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string? asOf = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _asOf = asOf;
    }

    public override async IAsyncEnumerable<ScenarioSchedulesv2DataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ScenarioSchedulesv2DataObject>? schedules = null;
        
        try
        {
            var response = await _apiClient.GetScenarioSchedulesV2Async(
                _projectId,
                _scenarioId,
                _asOf,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve scenario schedules v2. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            schedules = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving schedules v2 for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (schedules != null)
        {
            foreach (var schedule in schedules)
            {
                yield return schedule;
            }
        }
    }
}