using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Reporting.v1.ShouldStartFinishReport;

public class ShouldStartFinishReportDataReader : TypedAsyncDataReaderBase<ShouldStartFinishReportDataObject>
{
    private readonly ILogger<ShouldStartFinishReportDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;
    private readonly string _scenarioId;
    private readonly string _startDate;
    private readonly string _finishDate;

    public ShouldStartFinishReportDataReader(
        ILogger<ShouldStartFinishReportDataReader> logger,
        IApiClient apiClient,
        string projectId,
        string scenarioId,
        string startDate,
        string finishDate)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
        _scenarioId = scenarioId;
        _startDate = startDate;
        _finishDate = finishDate;
    }

    public override async IAsyncEnumerable<ShouldStartFinishReportDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ShouldStartFinishReportDataObject? report = null;

        try
        {
            var response = await _apiClient.GetShouldStartFinishReportAsync(
                _projectId,
                _scenarioId,
                _startDate,
                _finishDate,
                cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve should start/finish report. API StatusCode: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            report = response.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving should start/finish report for Project {ProjectId}, Scenario {ScenarioId}, Start Date {StartDate}, Finish Date {FinishDate}",
                _projectId, _scenarioId, _startDate, _finishDate);
            throw;
        }

        if (report != null)
        {
            // Ensure the key fields are set
            var enrichedReport = new ShouldStartFinishReportDataObject
            {
                ProjectId = _projectId,
                ScenarioId = _scenarioId,
                StartDate = _startDate,
                FinishDate = _finishDate,
                Total = report.Total,
                StartedLate = report.StartedLate,
                FinishedLate = report.FinishedLate,
                DidNotStart = report.DidNotStart,
                DidNotFinish = report.DidNotFinish,
                TotalOnTimeHitRate = report.TotalOnTimeHitRate,
                StartedOnTimeHitRate = report.StartedOnTimeHitRate,
                FinishedOnTimeHitRate = report.FinishedOnTimeHitRate,
                Activities = report.Activities
            };
            yield return enrichedReport;
        }
    }
}