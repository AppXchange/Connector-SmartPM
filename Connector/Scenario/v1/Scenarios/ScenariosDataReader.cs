using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Scenario.v1.Scenarios;

public class ScenariosDataReader : TypedAsyncDataReaderBase<ScenariosDataObject>
{
    private readonly ILogger<ScenariosDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string? _asOf;

    public ScenariosDataReader(
        ILogger<ScenariosDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string? asOf = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _asOf = asOf;
    }

    public override async IAsyncEnumerable<ScenariosDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ScenariosDataObject>? scenarios;

        try
        {
            var response = await _apiClient.GetProjectScenariosAsync(_projectId, _asOf, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project scenarios. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            scenarios = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching scenarios for project {ProjectId}", _projectId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching scenarios for project {ProjectId}", _projectId);
            throw;
        }

        if (scenarios == null || scenarios.Count == 0)
        {
            _logger.LogInformation("No scenarios found for project {ProjectId}", _projectId);
            yield break;
        }

        foreach (var scenario in scenarios)
        {
            yield return scenario;
        }
    }
}