namespace Connector.Projects.v1.ProjectWorkBreakdownStructureElement;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a specific work breakdown structure element from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Project Work Breakdown Structure Element data object representing a specific WBS element.")]
public class ProjectWorkBreakdownStructureElementDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the WBS element")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("parentWBSId")]
    [Description("ID of the parent WBS element, if any")]
    public int? ParentWBSId { get; init; }

    [JsonPropertyName("sequenceNumber")]
    [Description("Sequence number for ordering")]
    public int SequenceNumber { get; init; }

    [JsonPropertyName("code")]
    [Description("WBS code for the element")]
    [Required]
    public string Code { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("Name of the WBS element")]
    [Required]
    public string Name { get; init; } = string.Empty;
}