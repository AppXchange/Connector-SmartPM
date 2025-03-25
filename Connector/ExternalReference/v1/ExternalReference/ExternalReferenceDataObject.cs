namespace Connector.ExternalReference.v1.ExternalReference;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing external reference information from SmartPM
/// </summary>
[PrimaryKey("provider,externalId", nameof(Provider), nameof(ExternalId))]
[Description("SmartPM External Reference data object representing external system links for a project.")]
public class ExternalReferenceDataObject
{
    [JsonPropertyName("provider")]
    [Description("The external system provider name")]
    [Required]
    [MinLength(1)]
    public string Provider { get; init; } = string.Empty;

    [JsonPropertyName("externalId")]
    [Description("The ID of the resource in the external system")]
    [Required]
    [MinLength(1)]
    public string ExternalId { get; init; } = string.Empty;
}