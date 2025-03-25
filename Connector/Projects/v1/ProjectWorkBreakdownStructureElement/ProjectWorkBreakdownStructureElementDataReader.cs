using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Projects.v1.ProjectWorkBreakdownStructureElement;

public class ProjectWorkBreakdownStructureElementDataReader : TypedAsyncDataReaderBase<ProjectWorkBreakdownStructureElementDataObject>
{
    private readonly ILogger<ProjectWorkBreakdownStructureElementDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _wbsId;

    public ProjectWorkBreakdownStructureElementDataReader(
        ILogger<ProjectWorkBreakdownStructureElementDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string wbsId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _wbsId = wbsId;
    }

    public override async IAsyncEnumerable<ProjectWorkBreakdownStructureElementDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ProjectWorkBreakdownStructureElementDataObject? wbsElement = null;

        try
        {
            var response = await _apiClient.GetProjectWorkBreakdownStructureElementAsync(
                _projectId,
                _wbsId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve WBS element. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            wbsElement = response.GetData();
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching WBS element for project {ProjectId}, WBS {WbsId}",
                _projectId, _wbsId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching WBS element for project {ProjectId}, WBS {WbsId}",
                _projectId, _wbsId);
            throw;
        }

        if (wbsElement == null)
        {
            _logger.LogInformation("No WBS element found for project {ProjectId}, WBS {WbsId}",
                _projectId, _wbsId);
            yield break;
        }

        yield return wbsElement;
    }
}