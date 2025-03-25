namespace Connector.Activity.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ActivityV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ActivityV1ActionProcessorConfig>
{
    public override string ModuleId => "activity-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ActivityV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<ActivityV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ActivityV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ActivityV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ActivityV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, ActivityV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}