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

namespace Connector.Company.v1.CompanyConfig.Update;

public class UpdateCompanyConfigHandler : IActionHandler<UpdateCompanyConfigAction>
{
    private readonly ILogger<UpdateCompanyConfigHandler> _logger;
    private readonly IApiClient _apiClient;

    public UpdateCompanyConfigHandler(
        ILogger<UpdateCompanyConfigHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<UpdateCompanyConfigActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            _logger.LogError("Failed to deserialize action input");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[] { new Error { Source = new[] { "UpdateCompanyConfigHandler" }, Text = "Invalid input data" } }
            });
        }

        try
        {
            var response = await _apiClient.UpdateCompanyConfigurationAsync(
                input.Configuration,
                input.Setting,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = response.StatusCode.ToString(),
                    Errors = new[] { new Error { Source = new[] { "UpdateCompanyConfigHandler" }, Text = response.ErrorMessage ?? "Unknown error" } }
                });
            }

            var data = response.GetData();
            if (data == null)
            {
                throw new Exception("Update configuration response data was null");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(data);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, data));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection { DataObjectType = typeof(CompanyConfigDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(data, resultList);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error updating company configuration {Configuration}", input.Configuration);
            var errorSource = new List<string> { "UpdateCompanyConfigHandler" };
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
