namespace Connector.Scenario.v1.PrecentCompleteCurvev2;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a percent complete curve (v2) from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Percent Complete Curve v2 data object representing progress data points over time.")]
public class PrecentCompleteCurvev2DataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the curve data")]
    [Required]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("percentCompleteTypes")]
    [Description("Types of percent complete measurements")]
    [Required]
    public PercentCompleteTypesV2 PercentCompleteTypes { get; init; } = new();

    [JsonPropertyName("data")]
    [Description("Progress data points by date")]
    [Required]
    public List<ProgressDataPointV2> Data { get; init; } = new();
}

public class PercentCompleteTypesV2
{
    [JsonPropertyName("ACTUAL")]
    [Description("Label for actual progress")]
    public string Actual { get; init; } = string.Empty;

    [JsonPropertyName("SCHEDULED")]
    [Description("Label for scheduled completion")]
    public string Scheduled { get; init; } = string.Empty;

    [JsonPropertyName("PLANNED")]
    [Description("Label for planned progress")]
    public string Planned { get; init; } = string.Empty;
}

public class ProgressDataPointV2
{
    [JsonPropertyName("DATE")]
    [Description("Date of the progress measurement")]
    public string Date { get; init; } = string.Empty;

    [JsonPropertyName("ACTUAL")]
    [Description("Actual progress percentage")]
    public double? Actual { get; init; }

    [JsonPropertyName("SCHEDULED")]
    [Description("Scheduled progress percentage")]
    public double? Scheduled { get; init; }

    [JsonPropertyName("PLANNED")]
    [Description("Planned progress percentage")]
    public double? Planned { get; init; }
}