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

namespace Connector.Schedules.v1.ScenarioSchedules;

public class ScenarioSchedulesDataReader : TypedAsyncDataReaderBase<ScenarioSchedulesDataObject>
{
    private readonly ILogger<ScenarioSchedulesDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _asOf;

    public ScenarioSchedulesDataReader(
        ILogger<ScenarioSchedulesDataReader> logger,
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

    public override async IAsyncEnumerable<ScenarioSchedulesDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ScenarioSchedulesDataObject>? schedules = null;
        try
        {
            var response = await _apiClient.GetScenarioSchedulesAsync(
                _projectId,
                _scenarioId,
                _asOf,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve scenario schedules. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            schedules = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving schedules for project {ProjectId} and scenario {ScenarioId}",
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