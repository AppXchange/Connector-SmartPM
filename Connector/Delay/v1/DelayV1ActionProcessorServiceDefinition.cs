namespace Connector.Delay.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class DelayV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<DelayV1ActionProcessorConfig>
{
    public override string ModuleId => "delay-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<DelayV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<DelayV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<DelayV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<DelayV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<DelayV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, DelayV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}