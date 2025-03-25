using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using System.Runtime.CompilerServices;

namespace Connector.ExternalReference.v1.ExternalReference;

public class ExternalReferenceDataReader : TypedAsyncDataReaderBase<ExternalReferenceDataObject>
{
    private readonly ILogger<ExternalReferenceDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ExternalReferenceDataReader(
        ILogger<ExternalReferenceDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ExternalReferenceDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ExternalReferenceDataObject>? references = null;
        try
        {
            var response = await _apiClient.GetExternalReferencesAsync(_projectId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve external references. API StatusCode: {response.StatusCode}. Error: {response.ErrorMessage}");
            }

            references = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving external references for project {ProjectId}", _projectId);
            throw;
        }

        if (references != null)
        {
            foreach (var item in references)
            {
                yield return item;
            }
        }
    }
}