namespace Connector.Delay.v1.DelayTable;

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
[Description("SmartPM Delay Table data object representing delay information for a project scenario.")]
public class DelayTableDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("period")]
    [Description("The period number")]
    [Required]
    public int Period { get; init; }

    [JsonPropertyName("scheduleName")]
    [Description("The name of the schedule")]
    [Required]
    public string ScheduleName { get; init; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("The data date")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("endDate")]
    [Description("The end date")]
    [Required]
    public string EndDate { get; init; } = string.Empty;

    [JsonPropertyName("endDateVariance")]
    [Description("End date variance information")]
    [Required]
    public VarianceInfo EndDateVariance { get; init; } = new();

    [JsonPropertyName("criticalPathDelay")]
    [Description("Critical path delay information")]
    [Required]
    public VarianceInfo CriticalPathDelay { get; init; } = new();

    [JsonPropertyName("criticalPathRecovery")]
    [Description("Critical path recovery information")]
    [Required]
    public VarianceInfo CriticalPathRecovery { get; init; } = new();

    [JsonPropertyName("delayRecovery")]
    [Description("Delay recovery information")]
    [Required]
    public VarianceInfo DelayRecovery { get; init; } = new();

    [JsonPropertyName("filterId")]
    [Description("The filter ID")]
    public int? FilterId { get; init; }

    [JsonPropertyName("delays")]
    [Description("List of delays")]
    [Required]
    public List<DelayInfo> Delays { get; init; } = new();

    [JsonPropertyName("recoveries")]
    [Description("List of recoveries")]
    [Required]
    public List<RecoveryInfo> Recoveries { get; init; } = new();
}

public class VarianceInfo
{
    [JsonPropertyName("period")]
    [Description("The period value")]
    public int? Period { get; init; }

    [JsonPropertyName("cumulative")]
    [Description("The cumulative value")]
    [Required]
    public int Cumulative { get; init; }
}

public class DelayInfo
{
    [JsonPropertyName("activityId")]
    [Description("The activity ID")]
    [Required]
    public string ActivityId { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("The activity name")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("days")]
    [Description("Number of delay days")]
    [Required]
    public int Days { get; init; }
}

public class RecoveryInfo
{
    [JsonPropertyName("activityId")]
    [Description("The activity ID")]
    [Required]
    public string ActivityId { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("The activity name")]
    [Required]
    public string Name { get; init; } = string.Empty;
}