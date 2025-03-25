namespace Connector.Scenario.v1.ProjectHealthTrend;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing project health trend information from SmartPM
/// </summary>
[PrimaryKey("dataDate", nameof(DataDate))]
[Description("SmartPM Project Health Trend data object representing health metrics over time for a project schedule.")]
public class ProjectHealthTrendDataObject
{
    [JsonPropertyName("dataDate")]
    [Description("The schedule data date that was requested")]
    [Required]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("health")]
    [Description("The health score value")]
    [Required]
    public int Health { get; init; }

    [JsonPropertyName("risk")]
    [Description("Quality indicator for the project health (GOOD, FINE, or BAD)")]
    [Required]
    public string Risk { get; init; } = string.Empty;
}