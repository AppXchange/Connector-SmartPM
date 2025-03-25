namespace Connector.Projects.v1.Project.Delete;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for deleting a project in SmartPM
/// </summary>
[Description("Delete an existing project in SmartPM")]
public class DeleteProjectAction : IStandardAction<DeleteProjectActionInput, DeleteProjectActionOutput>
{
    public DeleteProjectActionInput ActionInput { get; set; } = new();
    public DeleteProjectActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();
    public bool CreateRtap => true;
}

public class DeleteProjectActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The ID of the project to delete")]
    [Required]
    public string ProjectId { get; set; } = string.Empty;
}

public class DeleteProjectActionOutput
{
    [JsonPropertyName("success")]
    [Description("Indicates if the project was successfully deleted")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    [Description("Response message from the API")]
    public string Message { get; set; } = string.Empty;
}
