using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Projects.v1.ProjectComments;

public class ProjectCommentsDataReader : TypedAsyncDataReaderBase<ProjectCommentsDataObject>
{
    private readonly ILogger<ProjectCommentsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ProjectCommentsDataReader(
        ILogger<ProjectCommentsDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ProjectCommentsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ProjectComment> comments;
        try
        {
            var response = await _apiClient.GetProjectCommentsAsync(_projectId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                _logger.LogError("Failed to retrieve project comments. API StatusCode: {StatusCode}, Error: {Error}",
                    response.StatusCode, response.ErrorMessage);
                throw new Exception($"Failed to retrieve project comments. API StatusCode: {response.StatusCode}");
            }

            comments = response.GetData() ?? new List<ProjectComment>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while retrieving project comments for project {ProjectId}", _projectId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while retrieving project comments for project {ProjectId}", _projectId);
            throw;
        }

        foreach (var comment in comments)
        {
            if (comment == null) continue;
            yield return new ProjectCommentsDataObject
            {
                Id = Guid.NewGuid(),
                Notes = comment.Notes,
                User = comment.User,
                CreatedAt = comment.CreatedAt,
                ProjectId = int.Parse(_projectId)
            };
        }
    }
}