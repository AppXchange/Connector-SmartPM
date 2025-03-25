using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Client.ConnectionDefinitions.Attributes;

namespace Connector;

/// <summary>
/// Contains all configuration values necessary for execution of the connector, that are configurable by a connector implementation.
/// </summary>
public class ConnectorRegistrationConfig
{
    [JsonPropertyName("companyId")]
    [ConnectionProperty(
        title: "Company ID",
        description: "Your SmartPM Company ID (found in the URL of your company dashboard)",
        isRequired: true,
        isSensitive: false)]
    public string CompanyId { get; set; } = string.Empty;
}
