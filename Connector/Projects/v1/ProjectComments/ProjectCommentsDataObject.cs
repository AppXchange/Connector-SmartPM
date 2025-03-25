namespace Connector.Projects.v1.ProjectComments;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a project comment from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Project comment data object representing notes and comments on a SmartPM project.")]
public class ProjectCommentsDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the comment")]
    [Required]
    public Guid Id { get; init; }

    [JsonPropertyName("notes")]
    [Description("Content of the comment")]
    [Required]
    public string Notes { get; init; } = string.Empty;

    [JsonPropertyName("user")]
    [Description("User who created the comment")]
    [Required]
    public string User { get; init; } = string.Empty;

    [JsonPropertyName("createdAt")]
    [Description("Timestamp when the comment was created")]
    [Required]
    public string CreatedAt { get; init; } = string.Empty;

    [JsonPropertyName("projectId")]
    [Description("ID of the project this comment belongs to")]
    [Required]
    public int ProjectId { get; init; }
}