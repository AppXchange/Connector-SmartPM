using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.ScheduleQuality.v1.QualityProfile;

public class QualityProfileDataReader : TypedAsyncDataReaderBase<QualityProfileDataObject>
{
    private readonly ILogger<QualityProfileDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _qualityProfileId;

    public QualityProfileDataReader(
        ILogger<QualityProfileDataReader> logger,
        IApiClient apiClient,
        string qualityProfileId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _qualityProfileId = qualityProfileId;
    }

    public override async IAsyncEnumerable<QualityProfileDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        QualityProfileDataObject? qualityProfile = null;

        try
        {
            var response = await _apiClient.GetQualityProfileAsync(_qualityProfileId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve quality profile. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            qualityProfile = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error retrieving quality profile with ID {QualityProfileId}", _qualityProfileId);
            throw;
        }

        if (qualityProfile != null)
        {
            yield return qualityProfile;
        }
    }
}