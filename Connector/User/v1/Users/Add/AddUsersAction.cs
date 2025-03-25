namespace Connector.User.v1.Users.Add;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for adding a user to a SmartPM project
/// </summary>
[Description("Add a user to a SmartPM project with a specified role")]
public class AddUsersAction : IStandardAction<AddUsersActionInput, UsersDataObject>
{
    public AddUsersActionInput ActionInput { get; set; } = new();
    public UsersDataObject ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class AddUsersActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The project to add the user to")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("user")]
    [Description("Email address of the user to add")]
    [Required]
    public string User { get; init; } = string.Empty;

    [JsonPropertyName("role")]
    [Description("Role to assign to the user")]
    [Required]
    public string Role { get; init; } = string.Empty;
}
