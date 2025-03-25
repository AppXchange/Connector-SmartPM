namespace Connector.User.v1;
using Connector.User.v1.CompanyUsers.Create;
using Connector.User.v1.CompanyUsers.Delete;
using Connector.User.v1.CompanyUsers.Update;
using Connector.User.v1.Users.Add;
using Connector.User.v1.Users.Remove;
using Connector.User.v1.Users.Update;
using Json.Schema.Generation;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Configuration for the Action Processor for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("User V1 Action Processor Configuration")]
[Description("Configuration of the data object actions for the module.")]
public class UserV1ActionProcessorConfig
{
    // Action Handler configuration
    public DefaultActionHandlerConfig AddUsersConfig { get; set; } = new();
    public DefaultActionHandlerConfig RemoveUsersConfig { get; set; } = new();
    public DefaultActionHandlerConfig UpdateUsersConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateCompanyUsersConfig { get; set; } = new();
    public DefaultActionHandlerConfig UpdateCompanyUsersConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteCompanyUsersConfig { get; set; } = new();
}