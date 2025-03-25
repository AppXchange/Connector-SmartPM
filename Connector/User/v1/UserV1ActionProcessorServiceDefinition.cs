namespace Connector.User.v1;
using Connector.User.v1.CompanyUsers;
using Connector.User.v1.CompanyUsers.Create;
using Connector.User.v1.CompanyUsers.Delete;
using Connector.User.v1.CompanyUsers.Update;
using Connector.User.v1.Users;
using Connector.User.v1.Users.Add;
using Connector.User.v1.Users.Remove;
using Connector.User.v1.Users.Update;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class UserV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<UserV1ActionProcessorConfig>
{
    public override string ModuleId => "user-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<UserV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<UserV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<UserV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<UserV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<UserV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<AddUsersHandler>();
        serviceCollection.AddScoped<RemoveUsersHandler>();
        serviceCollection.AddScoped<UpdateUsersHandler>();
        serviceCollection.AddScoped<CreateCompanyUsersHandler>();
        serviceCollection.AddScoped<UpdateCompanyUsersHandler>();
        serviceCollection.AddScoped<DeleteCompanyUsersHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, UserV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<AddUsersHandler, UsersDataObject>(ModuleId, "users", "add", config.AddUsersConfig);
        service.RegisterHandlerForDataObjectAction<RemoveUsersHandler, UsersDataObject>(ModuleId, "users", "remove", config.RemoveUsersConfig);
        service.RegisterHandlerForDataObjectAction<UpdateUsersHandler, UsersDataObject>(ModuleId, "users", "update", config.UpdateUsersConfig);
        service.RegisterHandlerForDataObjectAction<CreateCompanyUsersHandler, CompanyUsersDataObject>(ModuleId, "company-users", "create", config.CreateCompanyUsersConfig);
        service.RegisterHandlerForDataObjectAction<UpdateCompanyUsersHandler, CompanyUsersDataObject>(ModuleId, "company-users", "update", config.UpdateCompanyUsersConfig);
        service.RegisterHandlerForDataObjectAction<DeleteCompanyUsersHandler, CompanyUsersDataObject>(ModuleId, "company-users", "delete", config.DeleteCompanyUsersConfig);
    }
}