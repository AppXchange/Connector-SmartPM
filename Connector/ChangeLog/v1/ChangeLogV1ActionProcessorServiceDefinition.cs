namespace Connector.ChangeLog.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ChangeLogV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ChangeLogV1ActionProcessorConfig>
{
    public override string ModuleId => "changelog-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ChangeLogV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<ChangeLogV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ChangeLogV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ChangeLogV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ChangeLogV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, ChangeLogV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}