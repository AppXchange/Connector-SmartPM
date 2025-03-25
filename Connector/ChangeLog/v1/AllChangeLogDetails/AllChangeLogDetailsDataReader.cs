using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.ChangeLog.v1.AllChangeLogDetails;

public class AllChangeLogDetailsDataReader : TypedAsyncDataReaderBase<AllChangeLogDetailsDataObject>
{
    private readonly ILogger<AllChangeLogDetailsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string? _dataDate;

    public AllChangeLogDetailsDataReader(
        ILogger<AllChangeLogDetailsDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string? dataDate = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _dataDate = dataDate;
    }

    public override async IAsyncEnumerable<AllChangeLogDetailsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<AllChangeLogDetailsDataObject>? changeLogDetails = null;

        try
        {
            var response = await _apiClient.GetAllChangeLogDetailsAsync(
                _projectId,
                _scenarioId,
                _dataDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve change log details. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            changeLogDetails = response.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all change log details for Project {ProjectId}, Scenario {ScenarioId}",
                _projectId, _scenarioId);
            throw;
        }

        if (changeLogDetails != null)
        {
            foreach (var detail in changeLogDetails)
            {
                // Ensure the key fields are set
                var enrichedDetail = new AllChangeLogDetailsDataObject
                {
                    ProjectId = _projectId,
                    ScenarioId = _scenarioId,
                    Differences = detail.Differences,
                    Action = detail.Action,
                    Entity = detail.Entity,
                    FriendlyId = detail.FriendlyId,
                    AuditDate = detail.AuditDate,
                    FloatTotal = detail.FloatTotal,
                    ActivityIds = detail.ActivityIds
                };
                yield return enrichedDetail;
            }
        }
    }
}