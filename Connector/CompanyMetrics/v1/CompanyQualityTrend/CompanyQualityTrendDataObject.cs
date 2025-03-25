namespace Connector.CompanyMetrics.v1.CompanyQualityTrend;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing company quality trend information from the SmartPM API.
/// </summary>
[PrimaryKey("companyId,periodType,periodLabel", nameof(CompanyId), nameof(PeriodType), nameof(PeriodLabel))]
[Description("SmartPM Company Quality Trend data object representing quality metrics over time.")]
public class CompanyQualityTrendDataObject
{
    [JsonPropertyName("companyId")]
    [Description("The Company ID for the quality trend data")]
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

    [JsonPropertyName("score")]
    [Description("Quality score for the period (0-100)")]
    [Required]
    [Minimum(0)]
    [Maximum(100)]
    public int Score { get; init; }

    [JsonPropertyName("grade")]
    [Description("Grade information for the period")]
    [Required]
    public Grade Grade { get; init; } = new();
}

public class Grade
{
    [JsonPropertyName("mark")]
    [Description("Letter grade mark (e.g., 'B+', 'A-')")]
    [Required]
    public string Mark { get; init; } = string.Empty;

    [JsonPropertyName("indicator")]
    [Description("Performance indicator (e.g., 'GOOD', 'FINE')")]
    [Required]
    public string Indicator { get; init; } = string.Empty;
}