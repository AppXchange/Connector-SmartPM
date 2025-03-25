using Connector.Client;
using ESR.Hosting.Action;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Action;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Client.AppNetwork;

namespace Connector.Import.v1.ImportFile.Import;

public class ImportImportFileHandler : IActionHandler<ImportImportFileAction>
{
    private readonly ILogger<ImportImportFileHandler> _logger;
    private readonly IApiClient _apiClient;

    public ImportImportFileHandler(
        ILogger<ImportImportFileHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<ImportImportFileActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            throw new ArgumentException("Failed to deserialize action input");
        }

        try
        {
            var response = await _apiClient.ImportFilesAsync(
                input.ProjectId,
                input.Files,
                input.SendNotification,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to import files. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            var importResult = response.GetData();
            if (importResult == null)
            {
                throw new Exception("Import response was null");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(importResult);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, importResult));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(ImportFileDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(importResult, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { nameof(ImportImportFileHandler) };
            if (!string.IsNullOrEmpty(exception.Source)) 
            {
                errorSource.Add(exception.Source);
            }
            
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = exception.StatusCode?.ToString() ?? "500",
                Errors = new[]
                {
                    new Error
                    {
                        Source = errorSource.ToArray(),
                        Text = exception.Message
                    }
                }
            });
        }
    }
}
