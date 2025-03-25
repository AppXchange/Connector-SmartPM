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

namespace Connector.Import.v1.ImportFile;

public class ImportFileDataReader : TypedAsyncDataReaderBase<ImportFileDataObject>
{
    private readonly ILogger<ImportFileDataReader> _logger;
    private int _currentPage = 0;

    public ImportFileDataReader(
        ILogger<ImportFileDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<ImportFileDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<ImportFileDataObject>>();
            // If the ImportFileDataObject does not have the same structure as the ImportFile response from the API, create a new class for it and replace ImportFileDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<ImportFileResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<ImportFileDataObject>(
                //    relativeUrl: "importFiles",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ImportFileDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'ImportFileDataObject'. API StatusCode: {response.StatusCode}");
            }

            var data = response.GetData();
            if (data == null || !data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in data.Items)
            {
                // If new class was created to match the API response, create a new ImportFileDataObject object, map the properties and return a ImportFileDataObject.

                // Example:
                //var resource = new ImportFileDataObject
                //{
                //// TODO: Map properties.      
                //};
                //yield return resource;
                yield return item;
            }

            // Handle pagination per API client design
            _currentPage++;
            if (_currentPage >= data.TotalPages)
            {
                break;
            }
        }
    }
}