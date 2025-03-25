namespace Connector.Projects.v1.Project;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a specific project from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("SmartPM Project data object representing a specific construction project.")]
public class ProjectDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the project")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    [Description("Name of the project")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("startDate")]
    [Description("Start date of the project")]
    public string StartDate { get; init; } = string.Empty;

    [JsonPropertyName("country")]
    [Description("Country where the project is located")]
    public string Country { get; init; } = string.Empty;

    [JsonPropertyName("city")]
    [Description("City where the project is located")]
    public string City { get; init; } = string.Empty;

    [JsonPropertyName("state")]
    [Description("State/province where the project is located")]
    public string State { get; init; } = string.Empty;

    [JsonPropertyName("zipcode")]
    [Description("Postal/ZIP code of the project location")]
    public string Zipcode { get; init; } = string.Empty;

    [JsonPropertyName("latitude")]
    [Description("Latitude coordinate of the project location")]
    public double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    [Description("Longitude coordinate of the project location")]
    public double Longitude { get; init; }

    [JsonPropertyName("originalScenarioId")]
    [Description("ID of the original scenario for this project")]
    public int OriginalScenarioId { get; init; }

    [JsonPropertyName("defaultScenarioId")]
    [Description("ID of the default scenario for this project")]
    public int DefaultScenarioId { get; init; }

    [JsonPropertyName("underConstruction")]
    [Description("Indicates if the project is currently under construction")]
    public bool UnderConstruction { get; init; }

    [JsonPropertyName("projectPlanId")]
    [Description("Identifier for the project plan")]
    public string ProjectPlanId { get; init; } = string.Empty;

    [JsonPropertyName("projectPlanSlotCount")]
    [Description("Number of slots in the project plan")]
    public int ProjectPlanSlotCount { get; init; }

    [JsonPropertyName("weatherStationId")]
    [Description("ID of the associated weather station, if any")]
    public string? WeatherStationId { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Additional metadata associated with the project")]
    public Dictionary<string, object> Metadata { get; init; } = new();

    [JsonPropertyName("companyId")]
    [Description("ID of the company that owns the project")]
    public string CompanyId { get; init; } = string.Empty;

    [JsonPropertyName("source")]
    [Description("Source system of the project data")]
    public string Source { get; init; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("Date when the project data was last updated")]
    public string DataDate { get; init; } = string.Empty;
}