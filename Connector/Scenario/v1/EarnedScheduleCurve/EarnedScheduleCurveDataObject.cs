namespace Connector.Scenario.v1.EarnedScheduleCurve;

using Json.Schema.Generation;
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
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Example description of the object.")]
public class EarnedScheduleCurveDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the curve data")]
    [Required]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("data")]
    [Description("List of earned schedule data points")]
    [Required]
    public List<EarnedScheduleDataPoint> Data { get; init; } = new();
}

public class EarnedScheduleDataPoint
{
    [JsonPropertyName("date")]
    [Description("Date of the measurement")]
    public string Date { get; init; } = string.Empty;

    [JsonPropertyName("earnedDays")]
    [Description("Number of days earned")]
    public double EarnedDays { get; init; }
}