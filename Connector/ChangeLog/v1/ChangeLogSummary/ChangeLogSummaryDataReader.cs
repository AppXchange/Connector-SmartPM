using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.ChangeLog.v1.ChangeLogSummary;

public class ChangeLogSummaryDataReader : TypedAsyncDataReaderBase<ChangeLogSummaryDataObject>
{
    private readonly ILogger<ChangeLogSummaryDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;

    public ChangeLogSummaryDataReader(
        ILogger<ChangeLogSummaryDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
    }

    public override async IAsyncEnumerable<ChangeLogSummaryDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ChangeLogSummaryDataObject>? changeLogSummaries = null;

        try
        {
            var response = await _apiClient.GetChangeLogSummaryAsync(
                _projectId,
                _scenarioId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve change log summary. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            changeLogSummaries = response.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving change log summary for Project {ProjectId} and Scenario {ScenarioId}", _projectId, _scenarioId);
            throw;
        }

        if (changeLogSummaries != null)
        {
            foreach (var summary in changeLogSummaries)
            {
                yield return summary;
            }
        }
    }
}