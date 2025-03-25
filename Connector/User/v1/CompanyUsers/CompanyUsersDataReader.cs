using Connector.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using System.Runtime.CompilerServices;
using ESR.Hosting.CacheWriter;

namespace Connector.User.v1.CompanyUsers;

/// <summary>
/// Data reader for retrieving company users from SmartPM
/// </summary>
public class CompanyUsersDataReader : TypedAsyncDataReaderBase<CompanyUsersDataObject>
{
    private readonly ILogger<CompanyUsersDataReader> _logger;
    private readonly IApiClient _apiClient;

    public CompanyUsersDataReader(
        ILogger<CompanyUsersDataReader> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<CompanyUsersDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<CompanyUsersDataObject>? users = null;

        try
        {
            var response = await _apiClient.GetCompanyUsersAsync(cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                _logger.LogError("Failed to get company users. Status code: {StatusCode}, Error: {Error}",
                    response.StatusCode, response.ErrorMessage);
                yield break;
            }

            users = response.GetData();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error getting company users");
            yield break;
        }

        if (users == null || users.Count == 0)
        {
            _logger.LogWarning("No company users found");
            yield break;
        }

        foreach (var user in users)
        {
            yield return user;
        }
    }
}