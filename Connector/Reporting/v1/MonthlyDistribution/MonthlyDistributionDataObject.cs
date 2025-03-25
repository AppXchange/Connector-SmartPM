namespace Connector.Reporting.v1.MonthlyDistribution;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing monthly distribution of activities for a project scenario
/// </summary>
[PrimaryKey("projectId,scenarioId,date", nameof(ProjectId), nameof(ScenarioId), nameof(Date))]
[Description("SmartPM Monthly Distribution data object representing activity distribution data.")]
public class MonthlyDistributionDataObject
{
    [JsonPropertyName("projectId")]
    [Description("The Project ID containing the scenario")]
    [Required]
    public string ProjectId { get; init; } = string.Empty;

    [JsonPropertyName("scenarioId")]
    [Description("The Scenario ID for the distribution data")]
    [Required]
    public string ScenarioId { get; init; } = string.Empty;

    [JsonPropertyName("date")]
    [Description("The date for the distribution data")]
    [Required]
    public string Date { get; init; } = string.Empty;

    [JsonPropertyName("baselineStarts")]
    [Description("Number of activities that should start according to baseline")]
    [Required]
    public int BaselineStarts { get; init; }

    [JsonPropertyName("baselineFinishes")]
    [Description("Number of activities that should finish according to baseline")]
    [Required]
    public int BaselineFinishes { get; init; }

    [JsonPropertyName("currentStarts")]
    [Description("Number of activities that currently start")]
    [Required]
    public int CurrentStarts { get; init; }

    [JsonPropertyName("currentFinishes")]
    [Description("Number of activities that currently finish")]
    [Required]
    public int CurrentFinishes { get; init; }
}