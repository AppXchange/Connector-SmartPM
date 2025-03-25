using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.CompanyMetrics.v1.CompanyMetricTrend;

public class CompanyMetricTrendResponse
{
    [JsonPropertyName("companyMetricTrends")]
    public List<CompanyMetricTrendDataObject> CompanyMetricTrends { get; set; } = new();
}

public class CompanyMetricTrendDataReader : TypedAsyncDataReaderBase<CompanyMetricTrendDataObject>
{
    private readonly ILogger<CompanyMetricTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _metricType;
    private readonly string? _periodType;
    private readonly string? _filters;
    private readonly string? _qualityProfileId;

    public CompanyMetricTrendDataReader(
        ILogger<CompanyMetricTrendDataReader> logger,
        IApiClient apiClient,
        string metricType,
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _metricType = metricType;
        _periodType = periodType;
        _filters = filters;
        _qualityProfileId = qualityProfileId;
    }

    public override async IAsyncEnumerable<CompanyMetricTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        CompanyMetricTrendResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetCompanyMetricTrendAsync(
                _metricType,
                _periodType,
                _filters,
                _qualityProfileId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve company metric trend data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company metric trend data");
            throw;
        }

        if (response?.CompanyMetricTrends != null)
        {
            foreach (var trend in response.CompanyMetricTrends)
            {
                yield return trend;
            }
        }
    }
}