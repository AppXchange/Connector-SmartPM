namespace Connector.Reporting.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ReportingV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ReportingV1ActionProcessorConfig>
{
    public override string ModuleId => "reporting-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ReportingV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<ReportingV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ReportingV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ReportingV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ReportingV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, ReportingV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}