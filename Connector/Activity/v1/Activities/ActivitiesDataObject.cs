namespace Connector.Activity.v1.Activities;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing activity information from SmartPM
/// </summary>
[PrimaryKey("activityId", nameof(ActivityId))]
[Description("SmartPM Activity data object representing activity details for a project schedule.")]
public class ActivitiesDataObject
{
    [JsonPropertyName("activityId")]
    [Description("Unique identifier for the activity")]
    [Required]
    public string ActivityId { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("Name of the activity")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("percentCompleteType")]
    [Description("Type of percent complete measurement")]
    [Required]
    public string PercentCompleteType { get; init; } = string.Empty;

    [JsonPropertyName("durationPercentComplete")]
    [Description("Duration-based percent complete")]
    [Required]
    public double DurationPercentComplete { get; init; }

    [JsonPropertyName("physicalPercentComplete")]
    [Description("Physical percent complete")]
    [Required]
    public double PhysicalPercentComplete { get; init; }

    [JsonPropertyName("unitsPercentComplete")]
    [Description("Units-based percent complete")]
    [Required]
    public double UnitsPercentComplete { get; init; }

    [JsonPropertyName("floatTotal")]
    [Description("Total float")]
    public double? FloatTotal { get; init; }

    [JsonPropertyName("nativeFloatTotal")]
    [Description("Native total float")]
    public double? NativeFloatTotal { get; init; }

    [JsonPropertyName("floatFree")]
    [Description("Free float")]
    public double? FloatFree { get; init; }

    [JsonPropertyName("activityType")]
    [Description("Type of activity")]
    [Required]
    public string ActivityType { get; init; } = string.Empty;

    [JsonPropertyName("constraintType")]
    [Description("Type of schedule constraint")]
    [Required]
    public string ConstraintType { get; init; } = string.Empty;

    [JsonPropertyName("externalId")]
    [Description("External identifier")]
    [Required]
    public string ExternalId { get; init; } = string.Empty;

    [JsonPropertyName("plannedDuration")]
    [Description("Planned duration")]
    [Required]
    public double PlannedDuration { get; init; }

    [JsonPropertyName("remainingDuration")]
    [Description("Remaining duration")]
    [Required]
    public double RemainingDuration { get; init; }

    [JsonPropertyName("actualDuration")]
    [Description("Actual duration")]
    [Required]
    public double ActualDuration { get; init; }

    [JsonPropertyName("plannedBudget")]
    [Description("Planned budget")]
    [Required]
    public double PlannedBudget { get; init; }

    [JsonPropertyName("plannedManpower")]
    [Description("Planned manpower")]
    [Required]
    public double PlannedManpower { get; init; }

    [JsonPropertyName("baseline")]
    [Description("Baseline information")]
    [Required]
    public ActivityBaseline Baseline { get; init; } = new();

    [JsonPropertyName("inLongestPath")]
    [Description("Whether activity is in the longest path")]
    [Required]
    public bool InLongestPath { get; init; }

    [JsonPropertyName("retainLogic")]
    [Description("Whether to retain logic")]
    [Required]
    public bool RetainLogic { get; init; }

    [JsonPropertyName("startDate")]
    [Description("Start date")]
    [Required]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("finishDate")]
    [Description("Finish date")]
    [Required]
    public DateTime FinishDate { get; init; }

    [JsonPropertyName("lateStartDate")]
    [Description("Late start date")]
    [Required]
    public DateTime LateStartDate { get; init; }

    [JsonPropertyName("lateFinishDate")]
    [Description("Late finish date")]
    [Required]
    public DateTime LateFinishDate { get; init; }

    [JsonPropertyName("actualStartDate")]
    [Description("Actual start date")]
    [Required]
    public DateTime ActualStartDate { get; init; }

    [JsonPropertyName("actualFinishDate")]
    [Description("Actual finish date")]
    [Required]
    public DateTime ActualFinishDate { get; init; }

    [JsonPropertyName("constrainedDate")]
    [Description("Constrained date")]
    public DateTime? ConstrainedDate { get; init; }

    [JsonPropertyName("resumeDate")]
    [Description("Resume date")]
    public DateTime? ResumeDate { get; init; }

    [JsonPropertyName("sourceStartDate")]
    [Description("Source start date")]
    [Required]
    public DateTime SourceStartDate { get; init; }

    [JsonPropertyName("sourceFinishDate")]
    [Description("Source finish date")]
    [Required]
    public DateTime SourceFinishDate { get; init; }

    [JsonPropertyName("sourceActualStartDate")]
    [Description("Source actual start date")]
    [Required]
    public DateTime SourceActualStartDate { get; init; }

    [JsonPropertyName("sourceActualFinishDate")]
    [Description("Source actual finish date")]
    [Required]
    public DateTime SourceActualFinishDate { get; init; }

    [JsonPropertyName("percentComplete")]
    [Description("Overall percent complete")]
    [Required]
    public double PercentComplete { get; init; }
}

public class ActivityBaseline
{
    [JsonPropertyName("startDate")]
    [Description("Baseline start date")]
    [Required]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("finishDate")]
    [Description("Baseline finish date")]
    [Required]
    public DateTime FinishDate { get; init; }

    [JsonPropertyName("duration")]
    [Description("Baseline duration")]
    [Required]
    public double Duration { get; init; }

    [JsonPropertyName("critical")]
    [Description("Whether activity is critical in baseline")]
    [Required]
    public bool Critical { get; init; }

    [JsonPropertyName("type")]
    [Description("Baseline type")]
    [Required]
    public string Type { get; init; } = string.Empty;
}