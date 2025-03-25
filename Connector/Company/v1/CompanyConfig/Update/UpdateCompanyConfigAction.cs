namespace Connector.Company.v1.CompanyConfig.Update;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for updating a company configuration setting in SmartPM
/// </summary>
[Description("Update a company configuration setting")]
public class UpdateCompanyConfigAction : IStandardAction<UpdateCompanyConfigActionInput, UpdateCompanyConfigActionOutput>
{
    public UpdateCompanyConfigActionInput ActionInput { get; set; } = new();
    public UpdateCompanyConfigActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input model for updating a company configuration
/// </summary>
public class UpdateCompanyConfigActionInput
{
    [JsonPropertyName("configuration")]
    [Description("The configuration key to update")]
    [Required]
    public string Configuration { get; init; } = string.Empty;

    [JsonPropertyName("setting")]
    [Description("The new value for the configuration")]
    [Required]
    public string Setting { get; init; } = string.Empty;
}

/// <summary>
/// Output model containing the updated configuration
/// </summary>
public class UpdateCompanyConfigActionOutput
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
