namespace Connector.CompanyMetrics.v1;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class CompanyMetricsV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<CompanyMetricsV1ActionProcessorConfig>
{
    public override string ModuleId => "companymetrics-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<CompanyMetricsV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        var serviceConfig = JsonSerializer.Deserialize<CompanyMetricsV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<CompanyMetricsV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<CompanyMetricsV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<CompanyMetricsV1ActionProcessorConfig>>(this);

        // Register Action Handlers as scoped dependencies
    }

    public override void ConfigureService(IActionHandlerService service, CompanyMetricsV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
    }
}