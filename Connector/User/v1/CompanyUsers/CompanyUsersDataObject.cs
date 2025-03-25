namespace Connector.User.v1.CompanyUsers;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing company user information from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Company User data object representing user details and roles for a company.")]
public class CompanyUsersDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the user")]
    [Required]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("firstName")]
    [Description("First name of the user")]
    [Required]
    public string FirstName { get; init; } = string.Empty;

    [JsonPropertyName("lastName")]
    [Description("Last name of the user")]
    [Required]
    public string LastName { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    [Description("Email address of the user")]
    [Required]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    [Description("Phone number of the user")]
    public string? PhoneNumber { get; init; }

    [JsonPropertyName("title")]
    [Description("Title of the user")]
    public string? Title { get; init; }

    [JsonPropertyName("lastLogin")]
    [Description("UTC Timestamp of last login")]
    [Required]
    public DateTime LastLogin { get; init; }

    [JsonPropertyName("loginCount")]
    [Description("Number of times the user has logged in")]
    [Required]
    public int LoginCount { get; init; }

    [JsonPropertyName("role")]
    [Description("Role of the user in the company")]
    [Required]
    public string Role { get; init; } = string.Empty;

    [JsonPropertyName("ssoUser")]
    [Description("Whether the user is an SSO user")]
    public bool? SsoUser { get; init; }

    [JsonPropertyName("disableNotification")]
    [Description("Whether notifications are disabled for the SSO user")]
    public bool? DisableNotification { get; init; }
}