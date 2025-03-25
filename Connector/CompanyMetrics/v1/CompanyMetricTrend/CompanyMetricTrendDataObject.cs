namespace Connector.CompanyMetrics.v1.CompanyMetricTrend;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing company metric trend information from the SmartPM API.
/// </summary>
[PrimaryKey("companyId,periodType,periodLabel", nameof(CompanyId), nameof(PeriodType), nameof(PeriodLabel))]
[Description("SmartPM Company Metric Trend data object representing various metric values over time.")]
public class CompanyMetricTrendDataObject
{
    [JsonPropertyName("companyId")]
    [Description("The Company ID for the metric trend data")]
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

    [JsonPropertyName("metricValue")]
    [Description("The value of the metric for the period")]
    [Required]
    public decimal MetricValue { get; init; }
}