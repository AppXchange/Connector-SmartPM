using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;
using System.Threading.Tasks;

namespace Connector.Company.v1.CompanyConfig;

public class CompanyConfigDataReader : TypedAsyncDataReaderBase<CompanyConfigDataObject>
{
    private readonly ILogger<CompanyConfigDataReader> _logger;
    private readonly IApiClient _apiClient;

    public CompanyConfigDataReader(
        ILogger<CompanyConfigDataReader> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<CompanyConfigDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<CompanyConfigDataObject>? configurations = null;
        
        try
        {
            var response = await _apiClient.GetCompanyConfigurationAsync(cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve company configuration. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            configurations = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving company configuration");
            throw;
        }

        if (configurations != null)
        {
            foreach (var config in configurations)
            {
                yield return config;
            }
        }
    }
}