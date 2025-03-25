using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Scenario.v1.PrecentCompleteCurvev2;

public class PrecentCompleteCurvev2DataReader : TypedAsyncDataReaderBase<PrecentCompleteCurvev2DataObject>
{
    private readonly ILogger<PrecentCompleteCurvev2DataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly bool? _delta;

    public PrecentCompleteCurvev2DataReader(
        ILogger<PrecentCompleteCurvev2DataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        bool? delta = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _delta = delta;
    }

    public override async IAsyncEnumerable<PrecentCompleteCurvev2DataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        PrecentCompleteCurvev2DataObject? curveData = null;

        try
        {
            var response = await _apiClient.GetPercentCompleteCurveV2Async(
                _projectId,
                _scenarioId,
                _delta,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve percent complete curve v2. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            curveData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching percent complete curve v2 for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching percent complete curve v2 for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (curveData == null)
        {
            _logger.LogInformation("No percent complete curve v2 data found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        yield return curveData;
    }
}