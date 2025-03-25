namespace Connector.User.v1.CompanyUsers.Update;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for updating an existing company user in SmartPM
/// </summary>
[Description("Update an existing company user with specified details and role")]
public class UpdateCompanyUsersAction : IStandardAction<UpdateCompanyUsersActionInput, UpdateCompanyUsersActionOutput>
{
    public UpdateCompanyUsersActionInput ActionInput { get; set; } = new();
    public UpdateCompanyUsersActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input model for updating a company user
/// </summary>
public class UpdateCompanyUsersActionInput
{
    [JsonPropertyName("userId")]
    [Description("The ID of the user to update")]
    [Required]
    [MinLength(1)]
    public string UserId { get; init; } = string.Empty;

    [JsonPropertyName("firstName")]
    [Description("First name of the user")]
    [Required]
    [MinLength(1)]
    public string FirstName { get; init; } = string.Empty;

    [JsonPropertyName("lastName")]
    [Description("Last name of the user")]
    [Required]
    [MinLength(1)]
    public string LastName { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    [Description("Email address of the user")]
    [Required]
    [MinLength(1)]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    [Description("Phone number of the user")]
    public string? PhoneNumber { get; init; }

    [JsonPropertyName("role")]
    [Description("Role to assign to the user")]
    [Required]
    [MinLength(1)]
    public string Role { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    [Description("Title of the user")]
    [Required]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("ssoUser")]
    [Description("Whether the user is an SSO user")]
    public bool? SsoUser { get; init; }
}

/// <summary>
/// Output model containing the updated company user details
/// </summary>
public class UpdateCompanyUsersActionOutput
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the user")]
    [Required]
    [MinLength(1)]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("firstName")]
    [Description("First name of the user")]
    [Required]
    [MinLength(1)]
    public string FirstName { get; init; } = string.Empty;

    [JsonPropertyName("lastName")]
    [Description("Last name of the user")]
    [Required]
    [MinLength(1)]
    public string LastName { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    [Description("Email address of the user")]
    [Required]
    [MinLength(1)]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    [Description("Phone number of the user")]
    [Required]
    [MinLength(1)]
    public string PhoneNumber { get; init; } = string.Empty;

    [JsonPropertyName("role")]
    [Description("Role assigned to the user")]
    [Required]
    [MinLength(1)]
    public string Role { get; init; } = string.Empty;

    [JsonPropertyName("lastLogin")]
    [Description("UTC timestamp of last login")]
    [Required]
    [MinLength(1)]
    public string LastLogin { get; init; } = string.Empty;

    [JsonPropertyName("loginCount")]
    [Description("Number of times the user has logged in")]
    [Required]
    public int LoginCount { get; init; }
}
