namespace Connector.Import.v1.ImportFile;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing import file information from SmartPM
/// </summary>
[PrimaryKey("importId", nameof(ImportId))]
[Description("SmartPM Import File data object representing file import status.")]
public class ImportFileDataObject
{
    [JsonPropertyName("files")]
    [Description("Array of file import results")]
    [Required]
    public List<ImportFileResult> Files { get; init; } = new();

    [JsonPropertyName("importId")]
    [Description("Unique identifier for the import")]
    [Required]
    public string ImportId { get; init; } = string.Empty;

    [JsonPropertyName("status")]
    [Description("Status of the import")]
    [Required]
    public string Status { get; init; } = string.Empty;
}

public class ImportFileResult
{
    [JsonPropertyName("fileId")]
    [Description("ID of the imported file")]
    [Required]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("importId")]
    [Description("Import ID for this specific file")]
    [Required]
    public string ImportId { get; init; } = string.Empty;

    [JsonPropertyName("valid")]
    [Description("Whether the file is valid")]
    [Required]
    public bool Valid { get; init; }

    [JsonPropertyName("errors")]
    [Description("Array of error messages if any")]
    [Required]
    public List<string> Errors { get; init; } = new();
}