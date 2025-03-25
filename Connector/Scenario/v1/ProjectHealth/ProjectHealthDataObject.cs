namespace Connector.Scenario.v1.ProjectHealth;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing project health information from SmartPM
/// </summary>
[PrimaryKey("dataDate", nameof(DataDate))]
[Description("SmartPM Project Health data object representing health metrics for a project schedule.")]
public class ProjectHealthDataObject
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
    [Description("Quality indicator for the project health")]
    [Required]
    public string Risk { get; init; } = string.Empty;
}