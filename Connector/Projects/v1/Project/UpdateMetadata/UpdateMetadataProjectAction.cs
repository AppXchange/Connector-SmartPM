namespace Connector.Projects.v1.Project.UpdateMetadata;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for updating project metadata in SmartPM
/// </summary>
[Description("Update metadata fields and values for an existing project in SmartPM")]
public class UpdateMetadataProjectAction : IStandardAction<UpdateMetadataProjectActionInput, UpdateMetadataProjectActionOutput>
{
    public UpdateMetadataProjectActionInput ActionInput { get; set; } = new();
    public UpdateMetadataProjectActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();
    public bool CreateRtap => true;
}

public class UpdateMetadataProjectActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The ID of the project to update")]
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [JsonPropertyName("metadata")]
    [Description("Dictionary of metadata field names and their values")]
    [Required]
    public Dictionary<string, string> Metadata { get; set; } = new();
}

public class UpdateMetadataProjectActionOutput
{
    [JsonPropertyName("metadata")]
    [Description("Updated metadata fields and values")]
    public Dictionary<string, string> Metadata { get; set; } = new();
}
