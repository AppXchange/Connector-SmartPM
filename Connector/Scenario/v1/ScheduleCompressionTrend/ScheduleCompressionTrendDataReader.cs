using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;
using System.Linq;

namespace Connector.Scenario.v1.ScheduleCompressionTrend;

public class ScheduleCompressionTrendDataReader : TypedAsyncDataReaderBase<ScheduleCompressionTrendDataObject>
{
    private readonly ILogger<ScheduleCompressionTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public ScheduleCompressionTrendDataReader(
        ILogger<ScheduleCompressionTrendDataReader> logger,
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

    public override async IAsyncEnumerable<ScheduleCompressionTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ScheduleCompressionTrendDataObject>? trendData = null;

        try
        {
            var response = await _apiClient.GetScheduleCompressionTrendAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve schedule compression trend. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            trendData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving schedule compression trend data");
            throw;
        }

        if (trendData == null || !trendData.Any())
        {
            _logger.LogInformation("No schedule compression trend data found");
            yield break;
        }

        foreach (var dataPoint in trendData)
        {
            yield return dataPoint;
        }
    }
}