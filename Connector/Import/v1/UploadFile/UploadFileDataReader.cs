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

namespace Connector.Import.v1.UploadFile;

public class UploadFileDataReader : TypedAsyncDataReaderBase<UploadFileDataObject>
{
    private readonly ILogger<UploadFileDataReader> _logger;
    private int _currentPage = 0;

    public UploadFileDataReader(
        ILogger<UploadFileDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<UploadFileDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<UploadFileDataObject>>();
            // If the UploadFileDataObject does not have the same structure as the UploadFile response from the API, create a new class for it and replace UploadFileDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<UploadFileResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<UploadFileDataObject>(
                //    relativeUrl: "uploadFiles",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'UploadFileDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'UploadFileDataObject'. API StatusCode: {response.StatusCode}");
            }

            var data = response.GetData();
            if (data == null || !data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in data.Items)
            {
                // If new class was created to match the API response, create a new UploadFileDataObject object, map the properties and return a UploadFileDataObject.

                // Example:
                //var resource = new UploadFileDataObject
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