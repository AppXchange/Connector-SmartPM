namespace Connector.Scenario.v1.ScheduleCompressionTrend;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing schedule compression trend information from SmartPM
/// </summary>
[PrimaryKey("dataDate", nameof(DataDate))]
[Description("SmartPM Schedule Compression Trend data object representing compression metrics over time for a project schedule.")]
public class ScheduleCompressionTrendDataObject
{
    [JsonPropertyName("dataDate")]
    [Description("The schedule data date that was requested")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("scheduleCompression")]
    [Description("The schedule compression value")]
    [Required]
    public double ScheduleCompression { get; init; }

    [JsonPropertyName("scheduleCompressionIndex")]
    [Description("The schedule compression percentage")]
    [Required]
    public int ScheduleCompressionIndex { get; init; }

    [JsonPropertyName("indicator")]
    [Description("Quality indicator for the schedule compression")]
    [Required]
    public string Indicator { get; init; } = string.Empty;
}