namespace Connector.CompanyMetrics.v1;
using Connector.CompanyMetrics.v1.CompanyCompressionTrend;
using Connector.CompanyMetrics.v1.CompanyHealthTrend;
using Connector.CompanyMetrics.v1.CompanyMetricTrend;
using Connector.CompanyMetrics.v1.CompanyQualityTrend;
using ESR.Hosting.CacheWriter;
using Json.Schema.Generation;

/// <summary>
/// Configuration for the Cache writer for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("CompanyMetrics V1 Cache Writer Configuration")]
[Description("Configuration of the data object caches for the module.")]
public class CompanyMetricsV1CacheWriterConfig
{
    // Data Reader configuration
    public CacheWriterObjectConfig CompanyHealthTrendConfig { get; set; } = new();
    public CacheWriterObjectConfig CompanyQualityTrendConfig { get; set; } = new();
    public CacheWriterObjectConfig CompanyCompressionTrendConfig { get; set; } = new();
    public CacheWriterObjectConfig CompanyMetricTrendConfig { get; set; } = new();
}