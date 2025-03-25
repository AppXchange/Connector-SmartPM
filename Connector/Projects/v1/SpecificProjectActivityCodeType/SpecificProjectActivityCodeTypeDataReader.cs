using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Projects.v1.SpecificProjectActivityCodeType;

public class SpecificProjectActivityCodeTypeDataReader : TypedAsyncDataReaderBase<SpecificProjectActivityCodeTypeDataObject>
{
    private readonly ILogger<SpecificProjectActivityCodeTypeDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _activityCodeTypeId;
    private readonly string _activityCodeId;

    public SpecificProjectActivityCodeTypeDataReader(
        ILogger<SpecificProjectActivityCodeTypeDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string activityCodeTypeId,
        string activityCodeId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _activityCodeTypeId = activityCodeTypeId;
        _activityCodeId = activityCodeId;
    }

    public override async IAsyncEnumerable<SpecificProjectActivityCodeTypeDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        SpecificProjectActivityCodeTypeDataObject? activityCode = null;

        try
        {
            var response = await _apiClient.GetSpecificProjectActivityCodeTypeAsync(
                _projectId,
                _activityCodeTypeId,
                _activityCodeId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve specific project activity code. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            activityCode = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching specific activity code for project {ProjectId}, activity code type {ActivityCodeTypeId}, activity code {ActivityCodeId}",
                _projectId, _activityCodeTypeId, _activityCodeId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching specific activity code for project {ProjectId}, activity code type {ActivityCodeTypeId}, activity code {ActivityCodeId}",
                _projectId, _activityCodeTypeId, _activityCodeId);
            throw;
        }

        if (activityCode == null)
        {
            _logger.LogInformation("No activity code found for project {ProjectId}, activity code type {ActivityCodeTypeId}, activity code {ActivityCodeId}",
                _projectId, _activityCodeTypeId, _activityCodeId);
            yield break;
        }

        yield return activityCode;
    }
}