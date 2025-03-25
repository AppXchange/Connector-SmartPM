namespace Connector.Model.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ModelV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ModelV1ActionProcessorConfig>
{
    public override string ModuleId => "model-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ModelV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<ModelV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ModelV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ModelV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ModelV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, ModelV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}