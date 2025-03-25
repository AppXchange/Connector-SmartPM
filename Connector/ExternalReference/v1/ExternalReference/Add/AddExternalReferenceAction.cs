namespace Connector.ExternalReference.v1.ExternalReference.Add;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for adding an external reference to a SmartPM project
/// </summary>
[Description("Add an external reference link to a SmartPM project")]
public class AddExternalReferenceAction : IStandardAction<AddExternalReferenceActionInput, AddExternalReferenceActionOutput>
{
    public AddExternalReferenceActionInput ActionInput { get; set; } = new();
    public AddExternalReferenceActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class AddExternalReferenceActionInput
{
    [JsonPropertyName("projectId")]
    [Description("The project to add the external reference to")]
    [Required]
    [MinLength(1)]
    public string ProjectId { get; init; } = string.Empty;

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

public class AddExternalReferenceActionOutput
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
