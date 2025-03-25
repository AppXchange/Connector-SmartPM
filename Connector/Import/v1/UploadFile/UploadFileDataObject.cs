namespace Connector.Import.v1.UploadFile;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing upload file response from SmartPM
/// </summary>
[PrimaryKey("fileId", nameof(FileId))]
[Description("SmartPM Upload File data object representing the result of a file upload.")]
public class UploadFileDataObject
{
    [JsonPropertyName("fileId")]
    [Description("Unique identifier for the uploaded file")]
    [Required]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("message")]
    [Description("Optional message from the upload operation")]
    public string? Message { get; init; }
}