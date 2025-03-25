namespace Connector.ChangeLog.v1.ChangeLogDetails;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that will represent an object in the Xchange system. This will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[PrimaryKey("projectId,scenarioId,type,friendlyId", nameof(ProjectId), nameof(ScenarioId), nameof(Type), nameof(FriendlyId))]
[Description("SmartPM Change Log Details data object representing detailed changes for a specific change type.")]
public class ChangeLogDetailsDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID containing the scenario")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID for the change log")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    [Description("The type of changes being detailed")]
    [Required]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("differences")]
    [Description("The differences that happened for the activities")]
    [Required]
    public List<Difference> Differences { get; init; } = new();

    [JsonPropertyName("action")]
    [Description("The action that occurred on the activity (ADDED, DELETED, or UPDATED)")]
    [Required]
    public string Action { get; init; } = string.Empty;

    [JsonPropertyName("entity")]
    [Description("The entity that was updated (Activity, Logic, Calendar, or CalendarWorktime)")]
    [Required]
    public string Entity { get; init; } = string.Empty;

    [JsonPropertyName("friendlyId")]
    [Description("The friendly display name for the entity that was modified")]
    [Required]
    public string FriendlyId { get; init; } = string.Empty;

    [JsonPropertyName("auditDate")]
    [Description("The date the change was made")]
    [Required]
    public string AuditDate { get; init; } = string.Empty;

    [JsonPropertyName("floatTotal")]
    [Description("The lowest total float for an activity that was changed")]
    [Required]
    public int FloatTotal { get; init; }

    [JsonPropertyName("activityIds")]
    [Description("The IDs of activities that were impacted")]
    [Required]
    public List<string> ActivityIds { get; init; } = new();
}

public class Difference
{
    [JsonPropertyName("field")]
    [Description("The field that was updated")]
    [Required]
    public string Field { get; init; } = string.Empty;

    [JsonPropertyName("oldValue")]
    [Description("The original value of the field")]
    public object? OldValue { get; init; }

    [JsonPropertyName("newValue")]
    [Description("The updated value of the field")]
    [Required]
    public string NewValue { get; init; } = string.Empty;
}