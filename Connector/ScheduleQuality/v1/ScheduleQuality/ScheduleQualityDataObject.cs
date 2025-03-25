namespace Connector.ScheduleQuality.v1.ScheduleQuality;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;
using System.Collections.Generic;

/// <summary>
/// Data object that will represent an object in the Xchange system. This will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[PrimaryKey("projectId,scenarioId", nameof(ProjectId), nameof(ScenarioId))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Schedule Quality data object representing quality metrics and grades for a project scenario.")]
public class ScheduleQualityDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID containing the scenario")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID for the schedule quality")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("metrics")]
    [Description("Array of schedule quality metrics")]
    [Required]
    public List<ScheduleQualityMetric> Metrics { get; init; } = new();

    [JsonPropertyName("grade")]
    [Description("Overall grade for the schedule quality")]
    [Required]
    public ScheduleQualityGrade Grade { get; init; } = new();

    [JsonPropertyName("qualityProfileId")]
    [Description("ID of the quality profile used for assessment")]
    public string? QualityProfileId { get; init; }
}

public class ScheduleQualityMetric
{
    [JsonPropertyName("name")]
    [Description("Name of the metric")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("message")]
    [Description("Description or message about the metric")]
    [Required]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("value")]
    [Description("Numeric value of the metric")]
    [Required]
    public double Value { get; init; }

    [JsonPropertyName("normalizedValue")]
    [Description("Normalized value of the metric")]
    [Required]
    public double NormalizedValue { get; init; }

    [JsonPropertyName("indicator")]
    [Description("Quality indicator for the metric")]
    [Required]
    public string Indicator { get; init; } = string.Empty;
}

public class ScheduleQualityGrade
{
    [JsonPropertyName("mark")]
    [Description("Letter grade mark")]
    [Required]
    public string Mark { get; init; } = string.Empty;

    [JsonPropertyName("indicator")]
    [Description("Quality indicator for the grade")]
    [Required]
    public string Indicator { get; init; } = string.Empty;
}