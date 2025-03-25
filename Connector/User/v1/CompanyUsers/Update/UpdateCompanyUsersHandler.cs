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

namespace Connector.User.v1.CompanyUsers.Update;

public class UpdateCompanyUsersHandler : IActionHandler<UpdateCompanyUsersAction>
{
    private readonly ILogger<UpdateCompanyUsersHandler> _logger;
    private readonly IApiClient _apiClient;

    public UpdateCompanyUsersHandler(
        ILogger<UpdateCompanyUsersHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<UpdateCompanyUsersActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            _logger.LogError("Failed to deserialize action input");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[] { new Error { Source = new[] { "UpdateCompanyUsersHandler" }, Text = "Invalid input data" } }
            });
        }

        try
        {
            var response = await _apiClient.UpdateCompanyUserAsync(input.UserId, input, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = response.StatusCode.ToString(),
                    Errors = new[] { new Error { Source = new[] { "UpdateCompanyUsersHandler" }, Text = response.ErrorMessage ?? "Unknown error" } }
                });
            }

            var data = response.GetData();
            if (data == null)
            {
                throw new Exception("Update user response data was null");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(data);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, data));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(CompanyUsersDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(data, resultList);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error updating company user");
            var errorSource = new List<string> { "UpdateCompanyUsersHandler" };
            if (!string.IsNullOrEmpty(exception.Source)) errorSource.Add(exception.Source);
            
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
