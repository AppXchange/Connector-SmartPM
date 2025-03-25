using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.CompanyMetrics.v1.CompanyCompressionTrend;

public class CompanyCompressionTrendResponse
{
    [JsonPropertyName("companyCompressionTrends")]
    public List<CompanyCompressionTrendDataObject> CompanyCompressionTrends { get; set; } = new();
}

public class CompanyCompressionTrendDataReader : TypedAsyncDataReaderBase<CompanyCompressionTrendDataObject>
{
    private readonly ILogger<CompanyCompressionTrendDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string? _periodType;
    private readonly string? _filters;
    private readonly string? _qualityProfileId;

    public CompanyCompressionTrendDataReader(
        ILogger<CompanyCompressionTrendDataReader> logger,
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

    public override async IAsyncEnumerable<CompanyCompressionTrendDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        CompanyCompressionTrendResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetCompanyCompressionTrendAsync(
                _periodType,
                _filters,
                _qualityProfileId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve company compression trend data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company compression trend data");
            throw;
        }

        if (response?.CompanyCompressionTrends != null)
        {
            foreach (var trend in response.CompanyCompressionTrends)
            {
                yield return trend;
            }
        }
    }
}