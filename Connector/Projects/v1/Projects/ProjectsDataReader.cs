using Connector.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using ESR.Hosting.CacheWriter;

namespace Connector.Projects.v1.Projects;

public class ProjectsDataReader : TypedAsyncDataReaderBase<ProjectsDataObject>
{
    private readonly ILogger<ProjectsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private int _currentPage = 1;
    private const int PageSize = 100;

    public ProjectsDataReader(
        ILogger<ProjectsDataReader> logger,
        IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ProjectsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            List<ProjectsDataObject>? batch;

            try
            {
                batch = await GetNextBatchAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving projects");
                throw;
            }

            if (batch == null || !batch.Any())
            {
                yield break;
            }

            foreach (var project in batch)
            {
                yield return project;
            }

            if (batch.Count < PageSize)
            {
                yield break;
            }

            _currentPage++;
        }
    }

    private async Task<List<ProjectsDataObject>?> GetNextBatchAsync(CancellationToken cancellationToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "page", _currentPage.ToString() },
            { "pageSize", PageSize.ToString() }
        };

        var response = await _apiClient.GetAsync<List<ProjectsDataObject>>(
            "public/v1/projects",
            queryParams,
            cancellationToken)
            .ConfigureAwait(false);

        if (!response.IsSuccessful)
        {
            _logger.LogError("Failed to retrieve projects. Status code: {StatusCode}", response.StatusCode);
            throw new Exception($"Failed to retrieve projects. API StatusCode: {response.StatusCode}");
        }

        return response.GetData();
    }
}