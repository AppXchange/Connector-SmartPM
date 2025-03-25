using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Scenario.v1.PrecentCompleteCurve;

public class PrecentCompleteCurveDataReader : TypedAsyncDataReaderBase<PrecentCompleteCurveDataObject>
{
    private readonly ILogger<PrecentCompleteCurveDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;

    public PrecentCompleteCurveDataReader(
        ILogger<PrecentCompleteCurveDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
    }

    public override async IAsyncEnumerable<PrecentCompleteCurveDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        PrecentCompleteCurveDataObject? curveData = null;

        try
        {
            var response = await _apiClient.GetPercentCompleteCurveAsync(
                _projectId,
                _scenarioId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve percent complete curve. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            curveData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching percent complete curve for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching percent complete curve for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (curveData == null)
        {
            _logger.LogInformation("No percent complete curve data found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        yield return curveData;
    }
}