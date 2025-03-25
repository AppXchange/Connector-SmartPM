namespace Connector.Scenario.v1;
using Connector.Scenario.v1.EarnedScheduleCurve;
using Connector.Scenario.v1.PrecentCompleteCurve;
using Connector.Scenario.v1.PrecentCompleteCurvev2;
using Connector.Scenario.v1.ProjectHealth;
using Connector.Scenario.v1.ProjectHealthTrend;
using Connector.Scenario.v1.ScenarioDetails;
using Connector.Scenario.v1.Scenarios;
using Connector.Scenario.v1.ScheduleCompression;
using Connector.Scenario.v1.ScheduleCompressionTrend;
using Connector.Scenario.v1.SchedulePerformanceIndex;
using Connector.Scenario.v1.SchedulePerformanceIndexTrend;
using ESR.Hosting.CacheWriter;
using Json.Schema.Generation;

/// <summary>
/// Configuration for the Cache writer for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Scenario V1 Cache Writer Configuration")]
[Description("Configuration of the data object caches for the module.")]
public class ScenarioV1CacheWriterConfig
{
    // Data Reader configuration
    public CacheWriterObjectConfig ScenariosConfig { get; set; } = new();
    public CacheWriterObjectConfig ScenarioDetailsConfig { get; set; } = new();
    public CacheWriterObjectConfig PrecentCompleteCurveConfig { get; set; } = new();
    public CacheWriterObjectConfig PrecentCompleteCurvev2Config { get; set; } = new();
    public CacheWriterObjectConfig EarnedScheduleCurveConfig { get; set; } = new();
    public CacheWriterObjectConfig ScheduleCompressionConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectHealthConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectHealthTrendConfig { get; set; } = new();
    public CacheWriterObjectConfig ScheduleCompressionTrendConfig { get; set; } = new();
    public CacheWriterObjectConfig SchedulePerformanceIndexConfig { get; set; } = new();
    public CacheWriterObjectConfig SchedulePerformanceIndexTrendConfig { get; set; } = new();
}