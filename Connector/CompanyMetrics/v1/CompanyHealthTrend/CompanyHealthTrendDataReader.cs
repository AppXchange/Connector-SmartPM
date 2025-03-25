using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Text.Json.Serialization;

namespace Connector.CompanyMetrics.v1.CompanyHealthTrend;

public class CompanyHealthTrendResponse
{
    [JsonPropertyName("companyHealthTrends")]
    public List<CompanyHealthTrendDataObject> CompanyHealthTrends { get; set; } = new();
}

public class CompanyHealthTrendDataReader : TypedAsyncDataReaderBase<CompanyHealthTrendDataObject>
{
    private readonly ILogger<CompanyHealthTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string? _periodType;
    private readonly string? _filters;
    private readonly string? _qualityProfileId;

    public CompanyHealthTrendDataReader(
        ILogger<CompanyHealthTrendDataReader> logger,
        IApiClient apiClient,
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _periodType = periodType;
        _filters = filters;
        _qualityProfileId = qualityProfileId;
    }

    public override async IAsyncEnumerable<CompanyHealthTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        CompanyHealthTrendResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetCompanyHealthTrendAsync(
                _periodType,
                _filters,
                _qualityProfileId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve company health trend data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company health trend data");
            throw;
        }

        if (response?.CompanyHealthTrends != null)
        {
            foreach (var trend in response.CompanyHealthTrends)
            {
                yield return trend;
            }
        }
    }
}