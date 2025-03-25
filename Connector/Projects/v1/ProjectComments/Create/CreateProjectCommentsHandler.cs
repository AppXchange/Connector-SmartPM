using Connector.Client;
using ESR.Hosting.Action;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Action;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Client.AppNetwork;

namespace Connector.Projects.v1.ProjectComments.Create;

public class CreateProjectCommentsHandler : IActionHandler<CreateProjectCommentsAction>
{
    private readonly ILogger<CreateProjectCommentsHandler> _logger;
    private readonly IApiClient _apiClient;

    public CreateProjectCommentsHandler(
        ILogger<CreateProjectCommentsHandler> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CreateProjectCommentsActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "CreateProjectCommentsHandler" },
                        Text = "Invalid input data"
                    }
                }
            });
        }

        try
        {
            var response = await _apiClient.CreateProjectCommentAsync(input.ProjectId, input.Notes, cancellationToken)
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
                            Source = new[] { "CreateProjectCommentsHandler" },
                            Text = response.ErrorMessage ?? "Failed to create project comment"
                        }
                    }
                });
            }

            var output = new CreateProjectCommentsActionOutput
            {
                Comments = response.GetData()?.Select(c => new ProjectCommentResponse
                {
                    Notes = c.Notes,
                    User = c.User,
                    CreatedAt = c.CreatedAt
                }).ToList() ?? new List<ProjectCommentResponse>()
            };

            // Create sync operations for the new comment
            var operations = new List<SyncOperation>();
            foreach (var comment in output.Comments)
            {
                var commentObject = new ProjectCommentsDataObject
                {
                    Id = Guid.NewGuid(),
                    Notes = comment.Notes,
                    User = comment.User,
                    CreatedAt = comment.CreatedAt,
                    ProjectId = int.Parse(input.ProjectId)
                };

                var keyResolver = new DefaultDataObjectKey();
                var key = keyResolver.BuildKeyResolver()(commentObject);
                operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, commentObject));
            }

            var resultList = new List<CacheSyncCollection>
            {
                new() { DataObjectType = typeof(ProjectCommentsDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(output, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { "CreateProjectCommentsHandler" };
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
            _logger.LogError(ex, "Unexpected error while creating project comment");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "500",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "CreateProjectCommentsHandler" },
                        Text = "An unexpected error occurred"
                    }
                }
            });
        }
    }
}
