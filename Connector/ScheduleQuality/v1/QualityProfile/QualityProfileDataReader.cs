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

namespace Connector.ScheduleQuality.v1.QualityProfile;

public class QualityProfileDataReader : TypedAsyncDataReaderBase<QualityProfileDataObject>
{
    private readonly ILogger<QualityProfileDataReader> _logger;
    private int _currentPage = 0;

    public QualityProfileDataReader(
        ILogger<QualityProfileDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<QualityProfileDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<QualityProfileDataObject>>();
            // If the QualityProfileDataObject does not have the same structure as the QualityProfile response from the API, create a new class for it and replace QualityProfileDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<QualityProfileResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<QualityProfileDataObject>(
                //    relativeUrl: "qualityProfiles",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'QualityProfileDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'QualityProfileDataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new QualityProfileDataObject object, map the properties and return a QualityProfileDataObject.

                // Example:
                //var resource = new QualityProfileDataObject
                //{
                //// TODO: Map properties.      
                //};
                //yield return resource;
                yield return item;
            }

            // Handle pagination per API client design
            _currentPage++;
            if (_currentPage >= response.Data.TotalPages)
            {
                break;
            }
        }
    }
}