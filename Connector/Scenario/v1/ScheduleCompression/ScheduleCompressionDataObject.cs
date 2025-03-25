namespace Connector.Scenario.v1.ScheduleCompression;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing schedule compression information from SmartPM
/// </summary>
[PrimaryKey("dataDate", nameof(DataDate))]
[Description("SmartPM Schedule Compression data object representing compression metrics for a project schedule.")]
public class ScheduleCompressionDataObject
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