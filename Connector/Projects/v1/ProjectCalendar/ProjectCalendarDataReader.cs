using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Projects.v1.ProjectCalendar;

public class ProjectCalendarDataReader : TypedAsyncDataReaderBase<ProjectCalendarDataObject>
{
    private readonly ILogger<ProjectCalendarDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _calendarId;

    public ProjectCalendarDataReader(
        ILogger<ProjectCalendarDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string calendarId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _calendarId = calendarId;
    }

    public override async IAsyncEnumerable<ProjectCalendarDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ProjectCalendarDataObject? calendar;
        
        try
        {
            var response = await _apiClient.GetProjectCalendarAsync(_projectId, _calendarId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve project calendar. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            calendar = response.GetData();
            if (calendar == null)
            {
                _logger.LogInformation("No calendar found for project {ProjectId} and calendar {CalendarId}", _projectId, _calendarId);
                yield break;
            }
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while fetching project calendar for project {ProjectId} and calendar {CalendarId}", _projectId, _calendarId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching project calendar for project {ProjectId} and calendar {CalendarId}", _projectId, _calendarId);
            throw;
        }

        yield return calendar;
    }
}