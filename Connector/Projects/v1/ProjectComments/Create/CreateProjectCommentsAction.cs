namespace Connector.Projects.v1.ProjectComments.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for creating a new comment on a project in SmartPM
/// </summary>
[Description("Create a new comment on a project in SmartPM")]
public class CreateProjectCommentsAction : IStandardAction<CreateProjectCommentsActionInput, CreateProjectCommentsActionOutput>
{
    public CreateProjectCommentsActionInput ActionInput { get; set; } = new();
    public CreateProjectCommentsActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();
    public bool CreateRtap => true;
}

public class CreateProjectCommentsActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The ID of the project to add the comment to")]
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [JsonPropertyName("notes")]
    [Description("The content of the comment")]
    [Required]
    public string Notes { get; set; } = string.Empty;
}

public class CreateProjectCommentsActionOutput
{
    [JsonPropertyName("comments")]
    [Description("List of project comments including the newly created one")]
    public List<ProjectCommentResponse> Comments { get; set; } = new();
}

public class ProjectCommentResponse
{
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; } = string.Empty;
}
