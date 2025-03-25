using Connector.Client;
using ESR.Hosting.Action;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Action;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Client.AppNetwork;

namespace Connector.ExternalReference.v1.ExternalReference.Add;

public class AddExternalReferenceHandler : IActionHandler<AddExternalReferenceAction>
{
    private readonly ILogger<AddExternalReferenceHandler> _logger;
    private readonly IApiClient _apiClient;

    public AddExternalReferenceHandler(
        ILogger<AddExternalReferenceHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        AddExternalReferenceActionInput? input = null;
        try
        {
            input = JsonSerializer.Deserialize<AddExternalReferenceActionInput>(actionInstance.InputJson);
            if (input == null)
            {
                throw new JsonException("Failed to deserialize input");
            }

            var response = await _apiClient.AddExternalReferenceAsync(
                input.ProjectId,
                input.Provider,
                input.ExternalId,
                cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to add external reference. Status: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            var data = response.GetData();
            if (data == null || data.Count == 0)
            {
                throw new HttpRequestException("No data returned from successful external reference creation");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(data[0]);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, data[0]));

            var resultList = new List<CacheSyncCollection>
            {
                new() { DataObjectType = typeof(ExternalReferenceDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(data[0], resultList);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to deserialize input for project {ProjectId}", input?.ProjectId ?? "unknown");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "AddExternalReferenceHandler" },
                        Text = "Invalid input format: " + ex.Message
                    }
                }
            });
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to add external reference for project {ProjectId}", input?.ProjectId ?? "unknown");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = ex.StatusCode?.ToString() ?? "500",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "AddExternalReferenceHandler" },
                        Text = ex.Message
                    }
                }
            });
        }
    }
}
