using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Scenario.v1.SchedulePerformanceIndex;

public class SchedulePerformanceIndexDataReader : TypedAsyncDataReaderBase<SchedulePerformanceIndexDataObject>
{
    private readonly ILogger<SchedulePerformanceIndexDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public SchedulePerformanceIndexDataReader(
        ILogger<SchedulePerformanceIndexDataReader> logger,
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

    public override async IAsyncEnumerable<SchedulePerformanceIndexDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        SchedulePerformanceIndexDataObject? spiData = null;

        try
        {
            var response = await _apiClient.GetSchedulePerformanceIndexAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve schedule performance index. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            spiData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving schedule performance index data");
            throw;
        }

        if (spiData == null)
        {
            _logger.LogInformation("No schedule performance index data found");
            yield break;
        }

        yield return spiData;
    }
}