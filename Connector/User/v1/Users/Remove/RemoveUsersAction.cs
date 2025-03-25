namespace Connector.User.v1.Users.Remove;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for removing a user from a SmartPM project
/// </summary>
[Description("Remove a user from a SmartPM project")]
public class RemoveUsersAction : IStandardAction<RemoveUsersActionInput, RemoveUsersActionOutput>
{
    public RemoveUsersActionInput ActionInput { get; set; } = new();
    public RemoveUsersActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class RemoveUsersActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The project to remove the user from")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("userId")]
    [Description("The ID of the user to remove")]
    [Required]
    public string UserId { get; init; } = string.Empty;
}

public class RemoveUsersActionOutput
{
    [JsonPropertyName("success")]
    [Description("Whether the user was successfully removed")]
    [Required]
    public bool Success { get; init; }

    [JsonPropertyName("message")]
    [Description("Status message of the operation")]
    [Required]
    public string Message { get; init; } = string.Empty;
}
