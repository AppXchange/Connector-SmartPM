namespace Connector.Scenario.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ScenarioV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ScenarioV1ActionProcessorConfig>
{
    public override string ModuleId => "scenario-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ScenarioV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<ScenarioV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ScenarioV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ScenarioV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ScenarioV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, ScenarioV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}