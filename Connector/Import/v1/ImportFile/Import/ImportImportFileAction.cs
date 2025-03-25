namespace Connector.Import.v1.ImportFile.Import;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for importing files into SmartPM
/// </summary>
[Description("Import files into a SmartPM project")]
public class ImportImportFileAction : IStandardAction<ImportImportFileActionInput, ImportFileDataObject>
{
    public ImportImportFileActionInput ActionInput { get; set; } = new();
    public ImportFileDataObject ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class ImportFileRequest
{
    [JsonPropertyName("fileId")]
    [Description("The fileId provided by the /public/v1/upload endpoint")]
    [Required]
    public string FileId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("Name of the schedule being imported")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("Optional data date in the event that the file does not have a data date")]
    public string? DataDate { get; set; }
}

public class ImportImportFileActionInput
{
    [JsonPropertyName("files")]
    [Description("Array of files to import")]
    [Required]
    public List<ImportFileRequest> Files { get; set; } = new();

    [JsonPropertyName("sendNotification")]
    [Description("Send a notification whenever the delay analysis has completed")]
    public bool SendNotification { get; set; }

    [JsonPropertyName("projectId")]
    [Description("The project to import the files into")]
    [Required]
    public string ProjectId { get; set; } = string.Empty;
}
