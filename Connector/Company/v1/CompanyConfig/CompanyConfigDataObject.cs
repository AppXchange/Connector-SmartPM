namespace Connector.Company.v1.CompanyConfig;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing company configuration settings from SmartPM
/// </summary>
[PrimaryKey("configuration", nameof(Configuration))]
[Description("SmartPM Company Configuration data object representing system settings and permissions.")]
public class CompanyConfigDataObject
{
    [JsonPropertyName("configuration")]
    [Description("The configuration key")]
    [Required]
    public string Configuration { get; init; } = string.Empty;

    [JsonPropertyName("setting")]
    [Description("The value or setting for this configuration")]
    [Required]
    public string Setting { get; init; } = string.Empty;

    [JsonPropertyName("permission")]
    [Description("The permission level required for this configuration, if any")]
    public string? Permission { get; init; }
}