namespace Connector.Reporting.v1.ShouldStartFinishReport;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing should start/finish report information for a project scenario
/// </summary>
[PrimaryKey("projectId,scenarioId,startDate,finishDate", nameof(ProjectId), nameof(ScenarioId), nameof(StartDate), nameof(FinishDate))]
[Description("SmartPM Should Start/Finish Report data object representing activity progress between specified dates.")]
public class ShouldStartFinishReportDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID containing the scenario")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID for the report")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("startDate")]
    [Description("The start date of the window")]
    [Required]
    public string StartDate { get; init; } = string.Empty;

    [JsonPropertyName("finishDate")]
    [Description("The finish date of the window")]
    [Required]
    public string FinishDate { get; init; } = string.Empty;

    [JsonPropertyName("total")]
    [Description("The total number of activities that should have had some progress during the specified window")]
    [Required]
    public int Total { get; init; }

    [JsonPropertyName("startedLate")]
    [Description("The number of activities that started late")]
    [Required]
    public int StartedLate { get; init; }

    [JsonPropertyName("finishedLate")]
    [Description("The number of activities that finished late")]
    [Required]
    public int FinishedLate { get; init; }

    [JsonPropertyName("didNotStart")]
    [Description("The number of activities that did not start")]
    [Required]
    public int DidNotStart { get; init; }

    [JsonPropertyName("didNotFinish")]
    [Description("The number of activities that did not finish")]
    [Required]
    public int DidNotFinish { get; init; }

    [JsonPropertyName("totalOnTimeHitRate")]
    [Description("The percentage of activities that started/finished on time")]
    [Required]
    public double TotalOnTimeHitRate { get; init; }

    [JsonPropertyName("startedOnTimeHitRate")]
    [Description("The percentage of activities that started on time")]
    [Required]
    public double StartedOnTimeHitRate { get; init; }

    [JsonPropertyName("finishedOnTimeHitRate")]
    [Description("The percentage of activities that finished on time")]
    [Required]
    public double FinishedOnTimeHitRate { get; init; }

    [JsonPropertyName("activities")]
    [Description("The activity details")]
    [Required]
    public List<ActivityDetail> Activities { get; init; } = new();
}

public class ActivityDetail
{
    [JsonPropertyName("id")]
    [Description("The SmartPM id for the activity")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("customId")]
    [Description("The user friendly ID for the activity")]
    [Required]
    public string CustomId { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("The name of the activity")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("startingStartDate")]
    [Description("The start date of the activity at the start of the window")]
    [Required]
    public string StartingStartDate { get; init; } = string.Empty;

    [JsonPropertyName("startingFinishDate")]
    [Description("The finish date of the activity at the start of the window")]
    [Required]
    public string StartingFinishDate { get; init; } = string.Empty;

    [JsonPropertyName("startingPercentComplete")]
    [Description("The percent complete of the activity at the start of the window")]
    [Required]
    public double StartingPercentComplete { get; init; }

    [JsonPropertyName("startingTotalFloat")]
    [Description("The total float of the activity at the start of the window")]
    [Required]
    public int StartingTotalFloat { get; init; }

    [JsonPropertyName("startingActuallyStarted")]
    [Description("Was the activity actually started at the start of the window")]
    [Required]
    public bool StartingActuallyStarted { get; init; }

    [JsonPropertyName("startingActuallyFinished")]
    [Description("Was the activity actually finished at the start of the window")]
    [Required]
    public bool StartingActuallyFinished { get; init; }

    [JsonPropertyName("endingStartDate")]
    [Description("The start date of the activity at the end of the window")]
    [Required]
    public string EndingStartDate { get; init; } = string.Empty;

    [JsonPropertyName("didNotStart")]
    [Description("Did the activity not start?")]
    [Required]
    public bool DidNotStart { get; init; }

    [JsonPropertyName("lateStart")]
    [Description("Did the activity start late?")]
    [Required]
    public bool LateStart { get; init; }

    [JsonPropertyName("endingFinishDate")]
    [Description("The finish date of the activity at the end of the window")]
    [Required]
    public string EndingFinishDate { get; init; } = string.Empty;

    [JsonPropertyName("didNotFinish")]
    [Description("Did the activity not finish?")]
    [Required]
    public bool DidNotFinish { get; init; }

    [JsonPropertyName("lateFinish")]
    [Description("Did the activity finish late?")]
    [Required]
    public bool LateFinish { get; init; }

    [JsonPropertyName("endingPercentComplete")]
    [Description("The percent complete of the activity at the end of the window")]
    [Required]
    public double EndingPercentComplete { get; init; }

    [JsonPropertyName("endingTotalFloat")]
    [Description("The total float of the activity at the end of the window")]
    public int? EndingTotalFloat { get; init; }

    [JsonPropertyName("varianceStartDate")]
    [Description("What was the variance in start date between the start of the window and the end of the window")]
    [Required]
    public int VarianceStartDate { get; init; }

    [JsonPropertyName("varianceFinishDate")]
    [Description("What was the variance in finish date between the start of the window and the end of the window")]
    [Required]
    public int VarianceFinishDate { get; init; }

    [JsonPropertyName("variancePercentComplete")]
    [Description("What was the variance in percent complete between the start of the window and the end of the window")]
    [Required]
    public double VariancePercentComplete { get; init; }

    [JsonPropertyName("varianceTotalFloat")]
    [Description("What was the variance in total float between the start of the window and the end of the window")]
    public int? VarianceTotalFloat { get; init; }

    [JsonPropertyName("deleted")]
    [Description("Was this activity deleted during this time period?")]
    [Required]
    public bool Deleted { get; init; }

    [JsonPropertyName("endingActuallyStarted")]
    [Description("Was the activity actually started at the end of the window")]
    [Required]
    public bool EndingActuallyStarted { get; init; }

    [JsonPropertyName("endingActuallyFinished")]
    [Description("Was the activity actually finished at the end of the window")]
    [Required]
    public bool EndingActuallyFinished { get; init; }
}