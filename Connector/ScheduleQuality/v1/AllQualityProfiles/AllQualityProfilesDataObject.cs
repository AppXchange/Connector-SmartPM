namespace Connector.ScheduleQuality.v1.AllQualityProfiles;

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
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Example description of the object.")]
public class AllQualityProfilesDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier of the quality profile")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("companyId")]
    [Description("The company ID that owns this quality profile")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("name")]
    [Description("The name of the quality profile")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("scoreReporting")]
    [Description("The score reporting method for this profile")]
    [Required]
    public string ScoreReporting { get; init; } = string.Empty;

    [JsonPropertyName("qualityThreshold")]
    [Description("Array of quality thresholds for different metrics")]
    [Required]
    public List<QualityThreshold> QualityThreshold { get; init; } = new();

    [JsonPropertyName("configuration")]
    [Description("Configuration settings for the quality profile")]
    [Required]
    public Dictionary<string, ConfigurationSetting> Configuration { get; init; } = new();
}

public class QualityThreshold
{
    [JsonPropertyName("id")]
    [Description("The unique identifier of the threshold")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("metric")]
    [Description("The metric being measured")]
    [Required]
    public string Metric { get; init; } = string.Empty;

    [JsonPropertyName("impactType")]
    [Description("The type of impact this metric has")]
    [Required]
    public string ImpactType { get; init; } = string.Empty;

    [JsonPropertyName("fineImpact")]
    [Description("The fine impact value")]
    public double? FineImpact { get; init; }

    [JsonPropertyName("badImpact")]
    [Description("The bad impact value")]
    public double? BadImpact { get; init; }

    [JsonPropertyName("values")]
    [Description("Array of threshold values")]
    [Required]
    public List<ThresholdValue> Values { get; init; } = new();

    [JsonPropertyName("filters")]
    [Description("Array of filters applied to this threshold")]
    [Required]
    public List<Dictionary<string, object>> Filters { get; init; } = new();

    [JsonPropertyName("evaluationTarget")]
    [Description("The target for evaluation")]
    [Required]
    public string EvaluationTarget { get; init; } = string.Empty;

    [JsonPropertyName("isHidden")]
    [Description("Whether this threshold is hidden")]
    [Required]
    public bool IsHidden { get; init; }
}

public class ThresholdValue
{
    [JsonPropertyName("id")]
    [Description("The unique identifier of the threshold value")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("indicator")]
    [Description("The indicator for this threshold value")]
    [Required]
    public string Indicator { get; init; } = string.Empty;

    [JsonPropertyName("value")]
    [Description("The numeric value for this threshold")]
    public double? Value { get; init; }
}

public class ConfigurationSetting
{
    [JsonPropertyName("methodType")]
    [Description("The method type for this configuration setting")]
    [Required]
    public string MethodType { get; init; } = string.Empty;

    [JsonPropertyName("value")]
    [Description("The value for this configuration setting")]
    [Required]
    public string Value { get; init; } = string.Empty;
}