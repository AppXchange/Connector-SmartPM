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

namespace Connector.User.v1.Users.Remove;

public class RemoveUsersHandler : IActionHandler<RemoveUsersAction>
{
    private readonly ILogger<RemoveUsersHandler> _logger;
    private readonly IApiClient _apiClient;

    public RemoveUsersHandler(
        ILogger<RemoveUsersHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<RemoveUsersActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            throw new ArgumentException("Failed to deserialize action input");
        }

        try
        {
            var response = await _apiClient.RemoveProjectUserAsync(
                input.ProjectId,
                input.UserId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to remove user. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            var output = new RemoveUsersActionOutput
            {
                Success = true,
                Message = "User successfully removed from project"
            };

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(output);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Delete.ToString(), key.UrlPart, key.PropertyNames, output));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(UsersDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(output, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { nameof(RemoveUsersHandler) };
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
