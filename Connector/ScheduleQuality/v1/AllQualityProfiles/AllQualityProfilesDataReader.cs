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

namespace Connector.ScheduleQuality.v1.AllQualityProfiles;

public class AllQualityProfilesDataReader : TypedAsyncDataReaderBase<AllQualityProfilesDataObject>
{
    private readonly ILogger<AllQualityProfilesDataReader> _logger;
    private readonly IApiClient _apiClient;

    public AllQualityProfilesDataReader(
        ILogger<AllQualityProfilesDataReader> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<AllQualityProfilesDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<AllQualityProfilesDataObject>? qualityProfiles = null;

        try
        {
            var response = await _apiClient.GetAllQualityProfilesAsync(cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve quality profiles. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            qualityProfiles = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error retrieving quality profiles");
            throw;
        }

        if (qualityProfiles != null)
        {
            foreach (var profile in qualityProfiles)
            {
                yield return profile;
            }
        }
    }
}