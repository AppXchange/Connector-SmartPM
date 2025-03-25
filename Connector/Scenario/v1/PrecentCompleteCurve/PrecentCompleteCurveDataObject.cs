namespace Connector.Scenario.v1.PrecentCompleteCurve;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a percent complete curve from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Percent Complete Curve data object representing progress data points over time.")]
public class PrecentCompleteCurveDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the curve data")]
    [Required]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("percentCompleteTypes")]
    [Description("Types of percent complete measurements")]
    [Required]
    public PercentCompleteTypes PercentCompleteTypes { get; init; } = new();

    [JsonPropertyName("data")]
    [Description("Progress data points by date")]
    [Required]
    public Dictionary<string, ProgressDataPoint> Data { get; init; } = new();
}

public class PercentCompleteTypes
{
    [JsonPropertyName("ACTUAL")]
    public string Actual { get; init; } = string.Empty;

    [JsonPropertyName("SCHEDULED")]
    public string Scheduled { get; init; } = string.Empty;

    [JsonPropertyName("PLANNED")]
    public string Planned { get; init; } = string.Empty;

    [JsonPropertyName("PREDICTIVE")]
    public string Predictive { get; init; } = string.Empty;

    [JsonPropertyName("BASELINE_ID_PLANNED")]
    public string BaselineIdPlanned { get; init; } = string.Empty;
}

public class ProgressDataPoint
{
    [JsonPropertyName("ACTUAL")]
    public double Actual { get; init; }

    [JsonPropertyName("SCHEDULED")]
    public double Scheduled { get; init; }

    [JsonPropertyName("PLANNED")]
    public double Planned { get; init; }

    [JsonPropertyName("PREDICTIVE")]
    public double Predictive { get; init; }

    [JsonPropertyName("BASELINE_ID_PLANNED")]
    public double BaselineIdPlanned { get; init; }
}