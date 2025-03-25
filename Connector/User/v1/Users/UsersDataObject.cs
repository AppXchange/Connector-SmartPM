namespace Connector.User.v1.Users;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing user information from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM User data object representing user details and roles for a project.")]
public class UsersDataObject
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
    [Description("User's role in the project")]
    [Required]
    public string Role { get; init; } = string.Empty;
}