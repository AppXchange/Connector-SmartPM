namespace Connector.Schedules.v1.ScenarioSchedulesv2;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing schedule information for a scenario in SmartPM (v2)
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Scenario Schedule v2 data object representing schedule details for a project scenario.")]
public class ScenarioSchedulesv2DataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the schedule")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("fileName")]
    [Description("Name of the uploaded schedule file")]
    [Required]
    public string FileName { get; init; } = string.Empty;

    [JsonPropertyName("fileId")]
    [Description("Unique identifier for the uploaded file")]
    [Required]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("activityCount")]
    [Description("Number of activities in the schedule")]
    [Required]
    public int ActivityCount { get; init; }

    [JsonPropertyName("dataDate")]
    [Description("Data date of the schedule")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("baseline")]
    [Description("Is this schedule being used as a baseline for the analysis")]
    [Required]
    public bool Baseline { get; init; }

    [JsonPropertyName("sourceEndDate")]
    [Description("The end date that was in the source system")]
    [Required]
    public string SourceEndDate { get; init; } = string.Empty;
}