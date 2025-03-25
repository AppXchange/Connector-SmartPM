namespace Connector.Scenario.v1.SchedulePerformanceIndex;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing schedule performance index information from SmartPM
/// </summary>
[PrimaryKey("dataDate", nameof(DataDate))]
[Description("SmartPM Schedule Performance Index data object representing SPI metrics for a project schedule.")]
public class SchedulePerformanceIndexDataObject
{
    [JsonPropertyName("dataDate")]
    [Description("The schedule data date that was requested")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("spi")]
    [Description("The schedule performance index value")]
    [Required]
    public double Spi { get; init; }
}