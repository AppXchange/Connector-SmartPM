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

namespace Connector.Projects.v1.Project.Create;

public class CreateProjectHandler : IActionHandler<CreateProjectAction>
{
    private readonly ILogger<CreateProjectHandler> _logger;
    private readonly IApiClient _apiClient;

    public CreateProjectHandler(
        ILogger<CreateProjectHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CreateProjectActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "CreateProjectHandler" },
                        Text = "Invalid input data"
                    }
                }
            });
        }

        try
        {
            var response = await _apiClient.CreateProjectAsync(input, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = ((int)response.StatusCode).ToString(),
                    Errors = new[]
                    {
                        new Xchange.Connector.SDK.Action.Error
                        {
                            Source = new[] { "CreateProjectHandler" },
                            Text = response.ErrorMessage ?? "Failed to create project"
                        }
                    }
                });
            }

            var output = response.GetData();
            if (output == null)
            {
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = "500",
                    Errors = new[]
                    {
                        new Xchange.Connector.SDK.Action.Error
                        {
                            Source = new[] { "CreateProjectHandler" },
                            Text = "No response data received"
                        }
                    }
                });
            }

            // Get the full project details for sync
            var projectResponse = await _apiClient.GetProjectByIdAsync(output.ProjectId.ToString(), cancellationToken)
                .ConfigureAwait(false);

            if (!projectResponse.IsSuccessful || projectResponse.GetData() == null)
            {
                _logger.LogWarning("Project was created but failed to fetch full details. ProjectId: {ProjectId}", output.ProjectId);
                return ActionHandlerOutcome.Successful(output);
            }

            var project = projectResponse.GetData();
            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(project);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, project));

            var resultList = new List<CacheSyncCollection>
            {
                new() { DataObjectType = typeof(ProjectDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(output, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { "CreateProjectHandler" };
            if (!string.IsNullOrEmpty(exception.Source))
            {
                errorSource.Add(exception.Source);
            }
            
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = exception.StatusCode?.ToString() ?? "500",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = errorSource.ToArray(),
                        Text = exception.Message
                    }
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while creating project");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "500",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "CreateProjectHandler" },
                        Text = "An unexpected error occurred"
                    }
                }
            });
        }
    }
}
