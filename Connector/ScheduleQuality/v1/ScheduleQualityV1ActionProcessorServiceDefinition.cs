namespace Connector.ScheduleQuality.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ScheduleQualityV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ScheduleQualityV1ActionProcessorConfig>
{
    public override string ModuleId => "schedulequality-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ScheduleQualityV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<ScheduleQualityV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ScheduleQualityV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ScheduleQualityV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ScheduleQualityV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, ScheduleQualityV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}