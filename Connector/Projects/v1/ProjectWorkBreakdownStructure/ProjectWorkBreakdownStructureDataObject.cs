namespace Connector.Projects.v1.ProjectWorkBreakdownStructure;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a work breakdown structure element from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Project Work Breakdown Structure data object representing the hierarchical breakdown of project work.")]
public class ProjectWorkBreakdownStructureDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the WBS element")]
    [Required]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("code")]
    [Description("WBS code for the element")]
    [Required]
    public string Code { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("Name of the WBS element")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [Description("Detailed description of the WBS element")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("level")]
    [Description("Hierarchical level of the WBS element")]
    public int Level { get; init; }

    [JsonPropertyName("parentId")]
    [Description("ID of the parent WBS element")]
    public string? ParentId { get; init; }

    [JsonPropertyName("children")]
    [Description("List of child WBS elements")]
    public List<ProjectWorkBreakdownStructureDataObject> Children { get; init; } = new();

    [JsonPropertyName("metadata")]
    [Description("Additional metadata associated with the WBS element")]
    public Dictionary<string, string> Metadata { get; init; } = new();

    [JsonPropertyName("projectId")]
    [Description("ID of the project this WBS element belongs to")]
    [Required]
    public int ProjectId { get; init; }
}