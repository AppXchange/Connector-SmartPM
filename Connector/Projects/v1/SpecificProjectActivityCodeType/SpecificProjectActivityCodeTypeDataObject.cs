namespace Connector.Projects.v1.SpecificProjectActivityCodeType;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a specific activity code from a project's activity code type
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Specific Project Activity Code Type data object representing a single activity code within an activity code type.")]
public class SpecificProjectActivityCodeTypeDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the activity code")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("parent")]
    [Description("Parent activity code, if any")]
    public ActivityCodeParentReference? Parent { get; init; }

    [JsonPropertyName("value")]
    [Description("Value of the activity code")]
    [Required]
    public string Value { get; init; } = string.Empty;

    [JsonPropertyName("desc")]
    [Description("Description of the activity code")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("sequenceNumber")]
    [Description("Sequence number for ordering")]
    public int SequenceNumber { get; init; }

    [JsonPropertyName("order")]
    [Description("Order within the activity code type")]
    public int Order { get; init; }

    [JsonPropertyName("children")]
    [Description("Child activity codes, if any")]
    public List<ActivityCodeChildReference> Children { get; init; } = new();
}

/// <summary>
/// Reference to a parent activity code to avoid circular references
/// </summary>
public class ActivityCodeParentReference
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the parent activity code")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("value")]
    [Description("Value of the parent activity code")]
    [Required]
    public string Value { get; init; } = string.Empty;
}

/// <summary>
/// Reference to a child activity code to avoid circular references
/// </summary>
public class ActivityCodeChildReference
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the child activity code")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("value")]
    [Description("Value of the child activity code")]
    [Required]
    public string Value { get; init; } = string.Empty;

    [JsonPropertyName("sequenceNumber")]
    [Description("Sequence number for ordering")]
    public int SequenceNumber { get; init; }

    [JsonPropertyName("order")]
    [Description("Order within the activity code type")]
    public int Order { get; init; }
}