namespace Connector.Scenario.v1.Scenarios;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a scenario from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Scenario data object representing an analysis of project schedules.")]
public class ScenariosDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the scenario")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    [Description("Name of the scenario")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [Description("Description of the scenario")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("Date of the scenario data")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("scenarioType")]
    [Description("Type of scenario (COMPLETE or PARTIAL)")]
    [Required]
    public string ScenarioType { get; init; } = string.Empty;

    [JsonPropertyName("activityId")]
    [Description("ID of the activity being tracked (for PARTIAL scenarios)")]
    public string? ActivityId { get; init; }

    [JsonPropertyName("modelId")]
    [Description("ID of the associated model")]
    public int? ModelId { get; init; }
}