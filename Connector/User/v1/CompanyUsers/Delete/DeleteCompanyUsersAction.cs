namespace Connector.User.v1.CompanyUsers.Delete;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for deleting a company user in SmartPM
/// </summary>
[Description("Delete an existing company user")]
public class DeleteCompanyUsersAction : IStandardAction<DeleteCompanyUsersActionInput, DeleteCompanyUsersActionOutput>
{
    public DeleteCompanyUsersActionInput ActionInput { get; set; } = new();
    public DeleteCompanyUsersActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input model for deleting a company user
/// </summary>
public class DeleteCompanyUsersActionInput
{
    [JsonPropertyName("userId")]
    [Description("The ID of the user to delete")]
    [Required]
    [MinLength(1)]
    public string UserId { get; init; } = string.Empty;
}

/// <summary>
/// Output model containing the deletion status
/// </summary>
public class DeleteCompanyUsersActionOutput
{
    [JsonPropertyName("success")]
    [Description("Whether the user was successfully deleted")]
    [Required]
    public bool Success { get; init; }

    [JsonPropertyName("message")]
    [Description("Status message of the operation")]
    [Required]
    public string Message { get; init; } = string.Empty;
}
