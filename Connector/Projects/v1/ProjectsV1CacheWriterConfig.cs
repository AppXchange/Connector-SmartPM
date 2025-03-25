namespace Connector.Projects.v1;
using Connector.Projects.v1.Project;
using Connector.Projects.v1.ProjectActivityCodeTypes;
using Connector.Projects.v1.ProjectCalendar;
using Connector.Projects.v1.ProjectCalendars;
using Connector.Projects.v1.ProjectComments;
using Connector.Projects.v1.Projects;
using Connector.Projects.v1.ProjectWorkBreakdownStructure;
using Connector.Projects.v1.ProjectWorkBreakdownStructureElement;
using Connector.Projects.v1.SpecificProjectActivityCodeType;
using ESR.Hosting.CacheWriter;
using Json.Schema.Generation;

/// <summary>
/// Configuration for the Cache writer for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Projects V1 Cache Writer Configuration")]
[Description("Configuration of the data object caches for the module.")]
public class ProjectsV1CacheWriterConfig
{
    // Data Reader configuration
    public CacheWriterObjectConfig ProjectsConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectCommentsConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectCalendarsConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectCalendarConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectWorkBreakdownStructureConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectActivityCodeTypesConfig { get; set; } = new();
    public CacheWriterObjectConfig SpecificProjectActivityCodeTypeConfig { get; set; } = new();
    public CacheWriterObjectConfig ProjectWorkBreakdownStructureElementConfig { get; set; } = new();
}