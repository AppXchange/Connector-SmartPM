namespace Connector.Import.v1.UploadFile.Upload;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for uploading files to SmartPM
/// </summary>
[Description("Upload a file to SmartPM for later import")]
public class UploadUploadFileAction : IStandardAction<UploadUploadFileActionInput, UploadFileDataObject>
{
    public UploadUploadFileActionInput ActionInput { get; set; } = new();
    public UploadFileDataObject ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class UploadUploadFileActionInput
{
    [JsonPropertyName("fileUrl")]
    [Description("URL of the file to upload")]
    [Required]
    public string FileUrl { get; set; } = string.Empty;

    [JsonPropertyName("fileName")]
    [Description("Name of the file being uploaded")]
    [Required]
    public string FileName { get; set; } = string.Empty;
}
