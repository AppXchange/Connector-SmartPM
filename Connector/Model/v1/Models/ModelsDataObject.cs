namespace Connector.Model.v1.Models;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing model information from the SmartPM API.
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Model data object representing project model information.")]
public class ModelsDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the model")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    [Description("The name of the model")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [Description("Optional description of the model")]
    public string? Description { get; init; }

    [JsonPropertyName("isAutoUpdate")]
    [Description("Whether the model auto-updates")]
    [Required]
    public bool IsAutoUpdate { get; init; }

    [JsonPropertyName("isOriginalModel")]
    [Description("Whether this is the original model")]
    [Required]
    public bool IsOriginalModel { get; init; }

    [JsonPropertyName("isArchived")]
    [Description("Whether the model is archived")]
    [Required]
    public bool IsArchived { get; init; }

    [JsonPropertyName("modelType")]
    [Description("The type of model (BASELINE, ADVANCED_MODEL, TEST_WORKWEEK, REMOVE_RESOURCES, EXCLUDE_PROGRESS)")]
    [Required]
    public string ModelType { get; init; } = string.Empty;

    [JsonPropertyName("initialDataDate")]
    [Description("The initial data date of the model")]
    [Required]
    public DateTime InitialDataDate { get; init; }
}