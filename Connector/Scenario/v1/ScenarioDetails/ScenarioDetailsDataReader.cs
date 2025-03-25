using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Scenario.v1.ScenarioDetails;

public class ScenarioDetailsDataReader : TypedAsyncDataReaderBase<ScenarioDetailsDataObject>
{
    private readonly ILogger<ScenarioDetailsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public ScenarioDetailsDataReader(
        ILogger<ScenarioDetailsDataReader> logger,
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

    public override async IAsyncEnumerable<ScenarioDetailsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ScenarioDetailsDataObject? scenarioDetails;

        try
        {
            var response = await _apiClient.GetScenarioDetailsAsync(_projectId, _scenarioId, _dataDate, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve scenario details. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            scenarioDetails = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching scenario details for project {ProjectId} and scenario {ScenarioId}", _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching scenario details for project {ProjectId} and scenario {ScenarioId}", _projectId, _scenarioId);
            throw;
        }

        if (scenarioDetails == null)
        {
            _logger.LogInformation("No scenario details found for project {ProjectId} and scenario {ScenarioId}", _projectId, _scenarioId);
            yield break;
        }

        yield return scenarioDetails;
    }
}