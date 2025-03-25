using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Scenario.v1.EarnedScheduleCurve;

public class EarnedScheduleCurveDataReader : TypedAsyncDataReaderBase<EarnedScheduleCurveDataObject>
{
    private readonly ILogger<EarnedScheduleCurveDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;

    public EarnedScheduleCurveDataReader(
        ILogger<EarnedScheduleCurveDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
    }

    public override async IAsyncEnumerable<EarnedScheduleCurveDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        EarnedScheduleCurveDataObject? curveData = null;

        try
        {
            var response = await _apiClient.GetEarnedScheduleCurveAsync(
                _projectId,
                _scenarioId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve earned schedule curve. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            curveData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching earned schedule curve for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching earned schedule curve for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (curveData == null)
        {
            _logger.LogInformation("No earned schedule curve data found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        yield return curveData;
    }
}