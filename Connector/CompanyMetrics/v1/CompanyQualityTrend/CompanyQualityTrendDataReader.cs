using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Text.Json.Serialization;

namespace Connector.CompanyMetrics.v1.CompanyQualityTrend;

public class CompanyQualityTrendResponse
{
    [JsonPropertyName("companyQualityTrends")]
    public List<CompanyQualityTrendDataObject> CompanyQualityTrends { get; set; } = new();
}

public class CompanyQualityTrendDataReader : TypedAsyncDataReaderBase<CompanyQualityTrendDataObject>
{
    private readonly ILogger<CompanyQualityTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string? _periodType;
    private readonly string? _filters;
    private readonly string? _qualityProfileId;

    public CompanyQualityTrendDataReader(
        ILogger<CompanyQualityTrendDataReader> logger,
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

    public override async IAsyncEnumerable<CompanyQualityTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        CompanyQualityTrendResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetCompanyQualityTrendAsync(
                _periodType,
                _filters,
                _qualityProfileId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve company quality trend data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company quality trend data");
            throw;
        }

        if (response?.CompanyQualityTrends != null)
        {
            foreach (var trend in response.CompanyQualityTrends)
            {
                yield return trend;
            }
        }
    }
}