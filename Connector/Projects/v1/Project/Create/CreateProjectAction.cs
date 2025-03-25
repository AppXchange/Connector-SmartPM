namespace Connector.Projects.v1.Project.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action object that will represent an action in the Xchange system. This will contain an input object type,
/// an output object type, and a Action failure type (this will default to <see cref="StandardActionFailure"/>
/// but that can be overridden with your own preferred type). These objects will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[Description("Create a new project in SmartPM")]
public class CreateProjectAction : IStandardAction<CreateProjectActionInput, CreateProjectActionOutput>
{
    public CreateProjectActionInput ActionInput { get; set; } = new();
    public CreateProjectActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();
    public bool CreateRtap => true;
}

public class ProjectFile
{
    [JsonPropertyName("fileId")]
    [Description("Unique identifier of the file")]
    [Required]
    public string FileId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    [Description("Name of the file")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("dataDate")]
    [Description("Data date of the file")]
    public string DataDate { get; set; } = string.Empty;

    [JsonPropertyName("contractualEndDate")]
    [Description("Contractual end date of the file")]
    public string ContractualEndDate { get; set; } = string.Empty;
}

public enum ProjectSource
{
    XER,
    MPP,
    PMXML,
    PPX,
    PP
}

public enum ProjectPlanType
{
    Dashboard,
    Oversight,
    Controls
}

public class CreateProjectActionInput
{
    [JsonPropertyName("name")]
    [Description("Name of the project")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    [Description("City where the project is located")]
    [Required]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    [Description("State/province where the project is located")]
    [Required]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("zipCode")]
    [Description("Postal/ZIP code of the project location")]
    [Required]
    public string ZipCode { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    [Description("Country where the project is located")]
    [Required]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("latitude")]
    [Description("Latitude coordinate of the project location")]
    [Required]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    [Description("Longitude coordinate of the project location")]
    [Required]
    public double Longitude { get; set; }

    [JsonPropertyName("files")]
    [Description("The files that you have uploaded to SmartPM that you want imported to the project")]
    [Required]
    public List<ProjectFile> Files { get; set; } = new();

    [JsonPropertyName("source")]
    [Description("Source file format")]
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectSource Source { get; set; }

    [JsonPropertyName("projectPlan")]
    [Description("The plan type for the project")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectPlanType? ProjectPlan { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Additional metadata key-value pairs for the project")]
    public Dictionary<string, string>? Metadata { get; set; }
}

public class FileResponse
{
    [JsonPropertyName("fileId")]
    public string FileId { get; set; } = string.Empty;

    [JsonPropertyName("importId")]
    public string ImportId { get; set; } = string.Empty;

    [JsonPropertyName("valid")]
    public bool Valid { get; set; }

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new();
}

public class CreateProjectActionOutput
{
    [JsonPropertyName("projectId")]
    [Description("Unique identifier of the created project")]
    public int ProjectId { get; set; }

    [JsonPropertyName("message")]
    [Description("Response message from the API")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("files")]
    [Description("Status of the imported files")]
    public List<FileResponse> Files { get; set; } = new();
}
