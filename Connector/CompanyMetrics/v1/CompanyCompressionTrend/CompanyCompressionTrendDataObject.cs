namespace Connector.CompanyMetrics.v1.CompanyCompressionTrend;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing company compression trend information from the SmartPM API.
/// </summary>
[PrimaryKey("companyId,periodType,periodLabel", nameof(CompanyId), nameof(PeriodType), nameof(PeriodLabel))]
[Description("SmartPM Company Compression Trend data object representing compression metrics over time.")]
public class CompanyCompressionTrendDataObject
{
    [JsonPropertyName("companyId")]
    [Description("The Company ID for the compression trend data")]
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

    [JsonPropertyName("scheduleCompression")]
    [Description("Schedule compression value for the period")]
    [Required]
    public decimal ScheduleCompression { get; init; }

    [JsonPropertyName("indicator")]
    [Description("Performance indicator (e.g., 'GOOD', 'BAD')")]
    [Required]
    public string Indicator { get; init; } = string.Empty;
}