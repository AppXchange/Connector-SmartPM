using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Scenario.v1.ScheduleCompression;

public class ScheduleCompressionDataReader : TypedAsyncDataReaderBase<ScheduleCompressionDataObject>
{
    private readonly ILogger<ScheduleCompressionDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public ScheduleCompressionDataReader(
        ILogger<ScheduleCompressionDataReader> logger,
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

    public override async IAsyncEnumerable<ScheduleCompressionDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ScheduleCompressionDataObject? compressionData;

        try
        {
            var response = await _apiClient.GetScheduleCompressionAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve schedule compression. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            compressionData = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching schedule compression for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching schedule compression for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (compressionData == null)
        {
            _logger.LogInformation("No schedule compression data found for project {ProjectId} and scenario {ScenarioId}",
                _projectId, _scenarioId);
            yield break;
        }

        yield return compressionData;
    }
}