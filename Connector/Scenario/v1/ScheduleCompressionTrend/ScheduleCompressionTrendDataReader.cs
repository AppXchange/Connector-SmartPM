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

namespace Connector.Scenario.v1.ScheduleCompressionTrend;

public class ScheduleCompressionTrendDataReader : TypedAsyncDataReaderBase<ScheduleCompressionTrendDataObject>
{
    private readonly ILogger<ScheduleCompressionTrendDataReader> _logger;
    private int _currentPage = 0;

    public ScheduleCompressionTrendDataReader(
        ILogger<ScheduleCompressionTrendDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<ScheduleCompressionTrendDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<ScheduleCompressionTrendDataObject>>();
            // If the ScheduleCompressionTrendDataObject does not have the same structure as the ScheduleCompressionTrend response from the API, create a new class for it and replace ScheduleCompressionTrendDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<ScheduleCompressionTrendResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<ScheduleCompressionTrendDataObject>(
                //    relativeUrl: "scheduleCompressionTrends",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ScheduleCompressionTrendDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'ScheduleCompressionTrendDataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new ScheduleCompressionTrendDataObject object, map the properties and return a ScheduleCompressionTrendDataObject.

                // Example:
                //var resource = new ScheduleCompressionTrendDataObject
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