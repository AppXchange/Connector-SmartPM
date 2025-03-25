namespace Connector.CompanyMetrics.v1.CompanyHealthTrend;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing company health trend information from the SmartPM API.
/// </summary>
[PrimaryKey("companyId,periodType,periodLabel", nameof(CompanyId), nameof(PeriodType), nameof(PeriodLabel))]
[Description("SmartPM Company Health Trend data object representing health and risk metrics over time.")]
public class CompanyHealthTrendDataObject
{
    [JsonPropertyName("companyId")]
    [Description("The Company ID for the health trend data")]
    [Required]
    public string CompanyId { get; init; } = string.Empty;

    [JsonPropertyName("periodType")]
    [Description("The type of period for the trend data (BY_MONTH or BY_PERIOD)")]
    [Required]
    public string PeriodType { get; init; } = string.Empty;

    [JsonPropertyName("periodLabel")]
    [Description("Label for the period (e.g., '11/2022')")]
    [Required]
    public string PeriodLabel { get; init; } = string.Empty;

    [JsonPropertyName("health")]
    [Description("Health score for the period (0-100)")]
    [Required]
    [Minimum(0)]
    [Maximum(100)]
    public int Health { get; init; }

    [JsonPropertyName("risk")]
    [Description("Risk assessment for the period (e.g., 'GOOD', 'FINE')")]
    [Required]
    public string Risk { get; init; } = string.Empty;
}