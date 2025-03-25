namespace Connector.Projects.v1.ProjectActivityCodeTypes;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing an activity code type from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Project Activity Code Type data object representing activity categorization.")]
public class ProjectActivityCodeTypesDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the activity code type")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("sequenceNumber")]
    [Description("Sequence number for ordering")]
    [Required]
    public int SequenceNumber { get; init; }

    [JsonPropertyName("name")]
    [Description("Name of the activity code type")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("activityCodes")]
    [Description("List of activity codes associated with this type")]
    [Required]
    public List<ActivityCode> ActivityCodes { get; init; } = new();
}

public class ActivityCode
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the activity code")]
    public int Id { get; init; }

    [JsonPropertyName("value")]
    [Description("Value of the activity code")]
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
}