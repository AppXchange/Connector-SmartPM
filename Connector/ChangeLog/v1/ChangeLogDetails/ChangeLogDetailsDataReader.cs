using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.ChangeLog.v1.ChangeLogDetails;

public class ChangeLogDetailsDataReader : TypedAsyncDataReaderBase<ChangeLogDetailsDataObject>
{
    private readonly ILogger<ChangeLogDetailsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string _type;
    private readonly string? _dataDate;

    public ChangeLogDetailsDataReader(
        ILogger<ChangeLogDetailsDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string type,
        string? dataDate = null)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _type = type;
        _dataDate = dataDate;
    }

    public override async IAsyncEnumerable<ChangeLogDetailsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ChangeLogDetailsDataObject>? changeLogDetails = null;

        try
        {
            var response = await _apiClient.GetChangeLogDetailsAsync(
                _projectId,
                _scenarioId,
                _type,
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
            _logger.LogError(ex, "Error retrieving change log details for Project {ProjectId}, Scenario {ScenarioId}, Type {Type}",
                _projectId, _scenarioId, _type);
            throw;
        }

        if (changeLogDetails != null)
        {
            foreach (var detail in changeLogDetails)
            {
                // Ensure the key fields are set
                var enrichedDetail = new ChangeLogDetailsDataObject
                {
                    ProjectId = _projectId,
                    ScenarioId = _scenarioId,
                    Type = _type,
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