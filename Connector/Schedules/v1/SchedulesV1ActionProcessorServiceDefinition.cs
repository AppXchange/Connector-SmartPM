namespace Connector.Schedules.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class SchedulesV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<SchedulesV1ActionProcessorConfig>
{
    public override string ModuleId => "schedules-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<SchedulesV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<SchedulesV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<SchedulesV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<SchedulesV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<SchedulesV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, SchedulesV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}