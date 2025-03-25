using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Net.Http;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.User.v1.Users;

public class UsersDataReader : TypedAsyncDataReaderBase<UsersDataObject>
{
    private readonly ILogger<UsersDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public UsersDataReader(
        ILogger<UsersDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<UsersDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<UsersDataObject>? users = null;

        try
        {
            var response = await _apiClient.GetProjectUsersAsync(
                _projectId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project users. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            users = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving users for project {ProjectId}", _projectId);
            throw;
        }

        if (users == null)
        {
            _logger.LogInformation("No users found for project {ProjectId}", _projectId);
            yield break;
        }

        foreach (var user in users)
        {
            yield return user;
        }
    }
}