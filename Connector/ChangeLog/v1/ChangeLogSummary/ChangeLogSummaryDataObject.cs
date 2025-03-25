namespace Connector.ChangeLog.v1.ChangeLogSummary;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing change log summary information for a project scenario
/// </summary>
[PrimaryKey("projectId,scenarioId,dataDate", nameof(ProjectId), nameof(ScenarioId), nameof(DataDate))]
[Description("SmartPM Change Log Summary data object representing changes that have happened to a scenario over time.")]
public class ChangeLogSummaryDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID containing the scenario")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID for the change log")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("The date for which changes are summarized")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("metrics")]
    [Description("Metrics summarizing different types of changes")]
    [Required]
    public ChangeLogMetrics Metrics { get; init; } = new();
}

public class ChangeLogMetrics
{
    [JsonPropertyName("CriticalChanges")]
    [Description("The number of changes for an activity on the critical path for a specific period")]
    [Required]
    public int CriticalChanges { get; init; }

    [JsonPropertyName("LogicChanges")]
    [Description("The number of changes that happened to logic ties for a specific period")]
    [Required]
    public int LogicChanges { get; init; }

    [JsonPropertyName("DelayedActivityChanges")]
    [Description("The number of changes that happened to activities with delays for a specific period")]
    [Required]
    public int DelayedActivityChanges { get; init; }

    [JsonPropertyName("DurationChanges")]
    [Description("The number of activities where the duration changed for a specific period")]
    [Required]
    public int DurationChanges { get; init; }

    [JsonPropertyName("FlaggedChanges")]
    [Description("The number of changes that have been flagged for a specific period")]
    [Required]
    public int FlaggedChanges { get; init; }

    [JsonPropertyName("AllCalendarChanges")]
    [Description("The number of calendar / worktime changes that happened for a specific period")]
    [Required]
    public int AllCalendarChanges { get; init; }

    [JsonPropertyName("ActivityChanges")]
    [Description("The number of changes to activities for a specific period")]
    [Required]
    public int ActivityChanges { get; init; }

    [JsonPropertyName("CalendarChanges")]
    [Description("The number of changes to calendars for a specific period")]
    [Required]
    public int CalendarChanges { get; init; }

    [JsonPropertyName("WorkingDayChanges")]
    [Description("The number of worktime changes for a specific period")]
    [Required]
    public int WorkingDayChanges { get; init; }

    [JsonPropertyName("NearCriticalChanges")]
    [Description("The number of near critical (< 10 days of float) for a specific period")]
    [Required]
    public int NearCriticalChanges { get; init; }
}