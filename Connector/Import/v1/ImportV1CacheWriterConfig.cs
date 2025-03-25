namespace Connector.Import.v1;
using Connector.Import.v1.ImportFile;
using Connector.Import.v1.ImportStatus;
using Connector.Import.v1.UploadFile;
using ESR.Hosting.CacheWriter;
using Json.Schema.Generation;

/// <summary>
/// Configuration for the Cache writer for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Import V1 Cache Writer Configuration")]
[Description("Configuration of the data object caches for the module.")]
public class ImportV1CacheWriterConfig
{
    // Data Reader configuration
    public CacheWriterObjectConfig ImportFileConfig { get; set; } = new();
    public CacheWriterObjectConfig ImportStatusConfig { get; set; } = new();
    public CacheWriterObjectConfig UploadFileConfig { get; set; } = new();
}