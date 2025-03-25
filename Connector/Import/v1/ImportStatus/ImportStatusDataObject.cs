namespace Connector.Import.v1.ImportStatus;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing import status information from SmartPM
/// </summary>
[PrimaryKey("importId", nameof(ImportId))]
[Description("SmartPM Import Status data object representing the status of a file import.")]
public class ImportStatusDataObject
{
    [JsonPropertyName("message")]
    [Description("Status message of the import")]
    [Required]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("importId")]
    [Description("Unique identifier for the import")]
    [Required]
    public string ImportId { get; init; } = string.Empty;
}