namespace Connector.ScheduleQuality.v1.ScheduleQualityMetricDetails;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing schedule quality metric details from SmartPM
/// </summary>
[PrimaryKey("activityId", nameof(ActivityId))]
[Description("SmartPM Schedule Quality Metric Details data object representing activity-level quality metrics.")]
public class ScheduleQualityMetricDetailsDataObject
{
    [JsonPropertyName("@type")]
    [Description("The type of metric detail")]
    [Required]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("activityId")]
    [Description("The ID of the activity")]
    [Required]
    public string ActivityId { get; init; } = string.Empty;

    [JsonPropertyName("activityName")]
    [Description("The name of the activity")]
    [Required]
    public string ActivityName { get; init; } = string.Empty;

    [JsonPropertyName("value")]
    [Description("The metric value for this activity")]
    [Required]
    public int Value { get; init; }
}