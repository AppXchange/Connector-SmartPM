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

namespace Connector.Import.v1.UploadFile.Upload;

public class UploadUploadFileHandler : IActionHandler<UploadUploadFileAction>
{
    private readonly ILogger<UploadUploadFileHandler> _logger;
    private readonly IApiClient _apiClient;
    private readonly IHttpClientFactory _clientFactory;

    public UploadUploadFileHandler(
        ILogger<UploadUploadFileHandler> logger,
        IApiClient apiClient,
        IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _apiClient = apiClient;
        _clientFactory = clientFactory;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<UploadUploadFileActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            throw new ArgumentException("Failed to deserialize action input");
        }

        try
        {
            using var fileClient = _clientFactory.CreateClient();
            using var fileResponse = await fileClient.GetAsync(
                input.FileUrl,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken: cancellationToken);

            var response = await _apiClient.UploadFileAsync(
                input.FileName,
                await fileResponse.Content.ReadAsStreamAsync(cancellationToken),
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to upload file. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            var uploadResult = response.GetData();
            if (uploadResult == null)
            {
                throw new Exception("Upload response was null");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(uploadResult);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, uploadResult));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(UploadFileDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(uploadResult, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { nameof(UploadUploadFileHandler) };
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
