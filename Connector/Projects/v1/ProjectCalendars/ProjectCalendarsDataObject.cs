namespace Connector.Projects.v1.ProjectCalendars;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object representing a project calendar from SmartPM
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("SmartPM Project Calendar data object representing work schedules and holidays for a construction project.")]
public class ProjectCalendarsDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the calendar")]
    [Required]
    public int Id { get; init; }

    [JsonPropertyName("baseCalendarId")]
    [Description("ID of the base calendar, if this is derived from another calendar")]
    public int? BaseCalendarId { get; init; }

    [JsonPropertyName("name")]
    [Description("Name of the calendar")]
    [Required]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("hoursPerDay")]
    [Description("Standard working hours per day")]
    public int HoursPerDay { get; init; }

    [JsonPropertyName("hoursPerMonth")]
    [Description("Standard working hours per month")]
    public int HoursPerMonth { get; init; }

    [JsonPropertyName("hoursPerWeek")]
    [Description("Standard working hours per week")]
    public int HoursPerWeek { get; init; }

    [JsonPropertyName("hoursPerYear")]
    [Description("Standard working hours per year")]
    public int HoursPerYear { get; init; }

    [JsonPropertyName("worktimeList")]
    [Description("List of work time entries defining the calendar schedule")]
    public List<WorkTimeEntry> WorktimeList { get; init; } = new();
}

public class WorkTimeEntry
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the work time entry")]
    public int Id { get; init; }

    [JsonPropertyName("holiday")]
    [Description("Indicates if this entry represents a holiday")]
    public bool Holiday { get; init; }

    [JsonPropertyName("dayOfWeek")]
    [Description("Day of the week (0-6, where 0 is Sunday)")]
    public int DayOfWeek { get; init; }

    [JsonPropertyName("specificDate")]
    [Description("Specific date for this entry, if applicable")]
    public string? SpecificDate { get; init; }

    [JsonPropertyName("hours")]
    [Description("Total working hours for this entry")]
    public int Hours { get; init; }

    [JsonPropertyName("timeStart1")]
    [Description("First work period start time")]
    public string? TimeStart1 { get; init; }

    [JsonPropertyName("timeStart2")]
    [Description("Second work period start time")]
    public string? TimeStart2 { get; init; }

    [JsonPropertyName("timeStart3")]
    [Description("Third work period start time")]
    public string? TimeStart3 { get; init; }

    [JsonPropertyName("timeStart4")]
    [Description("Fourth work period start time")]
    public string? TimeStart4 { get; init; }

    [JsonPropertyName("timeEnd1")]
    [Description("First work period end time")]
    public string? TimeEnd1 { get; init; }

    [JsonPropertyName("timeEnd2")]
    [Description("Second work period end time")]
    public string? TimeEnd2 { get; init; }

    [JsonPropertyName("timeEnd3")]
    [Description("Third work period end time")]
    public string? TimeEnd3 { get; init; }

    [JsonPropertyName("timeEnd4")]
    [Description("Fourth work period end time")]
    public string? TimeEnd4 { get; init; }
}