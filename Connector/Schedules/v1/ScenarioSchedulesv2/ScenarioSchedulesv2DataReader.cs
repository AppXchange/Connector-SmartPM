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

namespace Connector.Schedules.v1.ScenarioSchedulesv2;

public class ScenarioSchedulesv2DataReader : TypedAsyncDataReaderBase<ScenarioSchedulesv2DataObject>
{
    private readonly ILogger<ScenarioSchedulesv2DataReader> _logger;
    private int _currentPage = 0;

    public ScenarioSchedulesv2DataReader(
        ILogger<ScenarioSchedulesv2DataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<ScenarioSchedulesv2DataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<ScenarioSchedulesv2DataObject>>();
            // If the ScenarioSchedulesv2DataObject does not have the same structure as the ScenarioSchedulesv2 response from the API, create a new class for it and replace ScenarioSchedulesv2DataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<ScenarioSchedulesv2Response>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<ScenarioSchedulesv2DataObject>(
                //    relativeUrl: "scenarioSchedulesv2s",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ScenarioSchedulesv2DataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'ScenarioSchedulesv2DataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new ScenarioSchedulesv2DataObject object, map the properties and return a ScenarioSchedulesv2DataObject.

                // Example:
                //var resource = new ScenarioSchedulesv2DataObject
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