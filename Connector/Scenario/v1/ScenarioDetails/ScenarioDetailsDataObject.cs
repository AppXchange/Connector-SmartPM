namespace Connector.Scenario.v1.ScenarioDetails;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing detailed scenario information from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Scenario Details data object representing comprehensive information about a project scenario.")]
public class ScenarioDetailsDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the scenario")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("dataDate")]
    [Description("Date of the scenario data")]
    public string DataDate { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("Name of the scenario")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [Description("Description of the scenario")]
    public string? Description { get; init; }

    [JsonPropertyName("scenarioType")]
    [Description("Type of the scenario")]
    public string ScenarioType { get; init; } = string.Empty;

    [JsonPropertyName("activityId")]
    [Description("ID of the associated activity")]
    public string? ActivityId { get; init; }

    [JsonPropertyName("plannedPercentComplete")]
    [Description("Planned percentage of completion")]
    public double PlannedPercentComplete { get; init; }

    [JsonPropertyName("actualPercentComplete")]
    [Description("Actual percentage of completion")]
    public double ActualPercentComplete { get; init; }

    [JsonPropertyName("forecastedCompletion")]
    [Description("Forecasted completion date")]
    public string? ForecastedCompletion { get; init; }

    [JsonPropertyName("scheduledCompletion")]
    [Description("Scheduled completion date")]
    public string ScheduledCompletion { get; init; } = string.Empty;

    [JsonPropertyName("scheduleCompression")]
    [Description("Schedule compression value")]
    public int ScheduleCompression { get; init; }

    [JsonPropertyName("scheduleQualityGrade")]
    [Description("Grade indicating schedule quality")]
    public string ScheduleQualityGrade { get; init; } = string.Empty;

    [JsonPropertyName("criticalPathDelay")]
    [Description("Delay in the critical path")]
    public int CriticalPathDelay { get; init; }

    [JsonPropertyName("endDateVariance")]
    [Description("Variance in the end date")]
    public int EndDateVariance { get; init; }

    [JsonPropertyName("futureRecovery")]
    [Description("Future recovery value")]
    public int FutureRecovery { get; init; }

    [JsonPropertyName("health")]
    [Description("Health indicator value")]
    public int Health { get; init; }

    [JsonPropertyName("schedulePerformanceIndex")]
    [Description("Schedule performance index")]
    public double SchedulePerformanceIndex { get; init; }

    [JsonPropertyName("baselineEndDate")]
    [Description("Baseline end date")]
    public string BaselineEndDate { get; init; } = string.Empty;

    [JsonPropertyName("contractualEndDate")]
    [Description("Contractual end date")]
    public string? ContractualEndDate { get; init; }

    [JsonPropertyName("plannedManpower")]
    [Description("Planned manpower value")]
    public int PlannedManpower { get; init; }

    [JsonPropertyName("earnedManpower")]
    [Description("Earned manpower value")]
    public int EarnedManpower { get; init; }

    [JsonPropertyName("remainingManpower")]
    [Description("Remaining manpower value")]
    public int RemainingManpower { get; init; }

    [JsonPropertyName("actualManpower")]
    [Description("Actual manpower value")]
    public int ActualManpower { get; init; }

    [JsonPropertyName("currentEstimatedManpower")]
    [Description("Current estimated manpower")]
    public int CurrentEstimatedManpower { get; init; }

    [JsonPropertyName("plannedBudget")]
    [Description("Planned budget value")]
    public int PlannedBudget { get; init; }

    [JsonPropertyName("remainingBudget")]
    [Description("Remaining budget value")]
    public int RemainingBudget { get; init; }

    [JsonPropertyName("earnedCost")]
    [Description("Earned cost value")]
    public int EarnedCost { get; init; }

    [JsonPropertyName("manpowerPlannedPercentComplete")]
    [Description("Planned percentage complete for manpower")]
    public double ManpowerPlannedPercentComplete { get; init; }

    [JsonPropertyName("manpowerActualPercentComplete")]
    [Description("Actual percentage complete for manpower")]
    public double ManpowerActualPercentComplete { get; init; }

    [JsonPropertyName("manpowerPerformanceIndex")]
    [Description("Performance index for manpower")]
    public double? ManpowerPerformanceIndex { get; init; }

    [JsonPropertyName("costPlannedPercentComplete")]
    [Description("Planned percentage complete for cost")]
    public double CostPlannedPercentComplete { get; init; }

    [JsonPropertyName("costActualPercentComplete")]
    [Description("Actual percentage complete for cost")]
    public double CostActualPercentComplete { get; init; }

    [JsonPropertyName("costPerformanceIndex")]
    [Description("Performance index for cost")]
    public double? CostPerformanceIndex { get; init; }

    [JsonPropertyName("resourceCount")]
    [Description("Count of resources")]
    public int ResourceCount { get; init; }

    [JsonPropertyName("averageDurationVariance")]
    [Description("Average variance in duration")]
    public int AverageDurationVariance { get; init; }
}