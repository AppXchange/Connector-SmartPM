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

namespace Connector.User.v1.CompanyUsers.Delete;

public class DeleteCompanyUsersHandler : IActionHandler<DeleteCompanyUsersAction>
{
    private readonly ILogger<DeleteCompanyUsersHandler> _logger;
    private readonly IApiClient _apiClient;

    public DeleteCompanyUsersHandler(
        ILogger<DeleteCompanyUsersHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<DeleteCompanyUsersActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            _logger.LogError("Failed to deserialize action input");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[] { new Error { Source = new[] { "DeleteCompanyUsersHandler" }, Text = "Invalid input data" } }
            });
        }

        try
        {
            var response = await _apiClient.DeleteCompanyUserAsync(input.UserId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = response.StatusCode.ToString(),
                    Errors = new[] { new Error { Source = new[] { "DeleteCompanyUsersHandler" }, Text = response.ErrorMessage ?? "Unknown error" } }
                });
            }

            var output = new DeleteCompanyUsersActionOutput
            {
                Success = true,
                Message = "User successfully deleted"
            };

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(output);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Delete.ToString(), key.UrlPart, key.PropertyNames, output));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(CompanyUsersDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(output, resultList);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error deleting company user");
            var errorSource = new List<string> { "DeleteCompanyUsersHandler" };
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
