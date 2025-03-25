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

namespace Connector.Activity.v1.Activities;

public class ActivitiesDataReader : TypedAsyncDataReaderBase<ActivitiesDataObject>
{
    private readonly ILogger<ActivitiesDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;
    private readonly string? _filterId;

    public ActivitiesDataReader(
        ILogger<ActivitiesDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string? dataDate = null,
        string? filterId = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _dataDate = dataDate;
        _filterId = filterId;
    }

    public override async IAsyncEnumerable<ActivitiesDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ActivitiesDataObject>? activities = null;

        try
        {
            var response = await _apiClient.GetActivitiesAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                _filterId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve activities. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            activities = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving activities for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (activities == null || !activities.Any())
        {
            _logger.LogInformation("No activities found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        foreach (var activity in activities)
        {
            yield return activity;
        }
    }
}