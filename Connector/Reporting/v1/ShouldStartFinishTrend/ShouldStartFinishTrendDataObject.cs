namespace Connector.Reporting.v1.ShouldStartFinishTrend;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that will represent an object in the Xchange system. This will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[PrimaryKey("projectId,scenarioId,dataDate", nameof(ProjectId), nameof(ScenarioId), nameof(DataDate))]
[Description("SmartPM Should Start/Finish Trend data object representing trend data for all schedules.")]
public class ShouldStartFinishTrendDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID containing the scenario")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID for the trend data")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("The data date in YYYY-MM-DDTHH:MM:SSZ format")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("total")]
    [Description("The total number of activities")]
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

    [JsonPropertyName("startedOnTime")]
    [Description("The number of activities that started on time")]
    [Required]
    public int StartedOnTime { get; init; }

    [JsonPropertyName("finishedOnTime")]
    [Description("The number of activities that finished on time")]
    [Required]
    public int FinishedOnTime { get; init; }

    [JsonPropertyName("startedOnTimeHitRate")]
    [Description("The percentage of activities that started on time")]
    [Required]
    public double StartedOnTimeHitRate { get; init; }

    [JsonPropertyName("finishedOnTimeHitRate")]
    [Description("The percentage of activities that finished on time")]
    [Required]
    public double FinishedOnTimeHitRate { get; init; }

    [JsonPropertyName("totalOnTimeHitRate")]
    [Description("The percentage of activities that started/finished on time")]
    [Required]
    public double TotalOnTimeHitRate { get; init; }
}