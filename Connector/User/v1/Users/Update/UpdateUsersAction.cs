namespace Connector.User.v1.Users.Update;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for updating a user's role in a SmartPM project
/// </summary>
[Description("Update a user's role in a SmartPM project")]
public class UpdateUsersAction : IStandardAction<UpdateUsersActionInput, UpdateUsersActionOutput>
{
    public UpdateUsersActionInput ActionInput { get; set; } = new();
    public UpdateUsersActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class UpdateUsersActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The project to update the user in")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("userId")]
    [Description("The ID of the user to update")]
    [Required]
    public string UserId { get; init; } = string.Empty;

    [JsonPropertyName("user")]
    [Description("Email address of the user")]
    [Required]
    public string User { get; init; } = string.Empty;

    [JsonPropertyName("role")]
    [Description("New role to assign to the user")]
    [Required]
    public string Role { get; init; } = string.Empty;
}

public class UpdateUsersActionOutput
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the user")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("user")]
    [Description("User email address")]
    [Required]
    public string User { get; init; } = string.Empty;

    [JsonPropertyName("role")]
    [Description("Updated role in the project")]
    [Required]
    public string Role { get; init; } = string.Empty;
}
