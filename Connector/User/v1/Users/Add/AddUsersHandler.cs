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

namespace Connector.User.v1.Users.Add;

public class AddUsersHandler : IActionHandler<AddUsersAction>
{
    private readonly ILogger<AddUsersHandler> _logger;
    private readonly IApiClient _apiClient;

    public AddUsersHandler(
        ILogger<AddUsersHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<AddUsersActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            throw new ArgumentException("Failed to deserialize action input");
        }

        try
        {
            var response = await _apiClient.AddProjectUserAsync(
                input.ProjectId,
                input.User,
                input.Role,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to add user. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            var addedUser = response.GetData();
            if (addedUser == null)
            {
                throw new Exception("Add user response was null");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(addedUser);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, addedUser));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(UsersDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(addedUser, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { nameof(AddUsersHandler) };
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
