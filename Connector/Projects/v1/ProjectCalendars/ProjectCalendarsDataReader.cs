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

namespace Connector.Projects.v1.ProjectCalendars;

public class ProjectCalendarsDataReader : TypedAsyncDataReaderBase<ProjectCalendarsDataObject>
{
    private readonly ILogger<ProjectCalendarsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ProjectCalendarsDataReader(
        ILogger<ProjectCalendarsDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ProjectCalendarsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        List<ProjectCalendarsDataObject>? calendars;
        
        try
        {
            var response = await _apiClient.GetProjectCalendarsAsync(_projectId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project calendars. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            calendars = response.GetData();
            if (calendars == null || !calendars.Any())
            {
                _logger.LogInformation("No calendars found for project {ProjectId}", _projectId);
                yield break;
            }
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching project calendars for project {ProjectId}", _projectId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching project calendars for project {ProjectId}", _projectId);
            throw;
        }

        foreach (var calendar in calendars)
        {
            yield return calendar;
        }
    }
}