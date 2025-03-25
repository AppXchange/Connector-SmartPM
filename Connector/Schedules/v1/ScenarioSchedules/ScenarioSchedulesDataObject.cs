namespace Connector.Schedules.v1.ScenarioSchedules;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing schedule information for a scenario in SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Scenario Schedule data object representing schedule details for a project scenario.")]
public class ScenarioSchedulesDataObject
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

    [JsonPropertyName("dataDate")]
    [Description("Data date of the schedule in YYYY-MM-DD format")]
    [Required]
    public string DataDate { get; init; } = string.Empty;
}