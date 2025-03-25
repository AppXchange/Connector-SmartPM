using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Import.v1.ImportStatus;

public class ImportStatusDataReader : TypedAsyncDataReaderBase<ImportStatusDataObject>
{
    private readonly ILogger<ImportStatusDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _importId;

    public ImportStatusDataReader(
        ILogger<ImportStatusDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string importId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _importId = importId;
    }

    public override async IAsyncEnumerable<ImportStatusDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ImportStatusDataObject? importStatus = null;

        try
        {
            var response = await _apiClient.GetImportStatusAsync(
                _projectId,
                _importId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve import status. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            importStatus = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving import status for project {ProjectId} and import {ImportId}",
                _projectId, _importId);
            throw;
        }

        if (importStatus == null)
        {
            _logger.LogInformation("No import status found for project {ProjectId} and import {ImportId}",
                _projectId, _importId);
            yield break;
        }

        yield return importStatus;
    }
}