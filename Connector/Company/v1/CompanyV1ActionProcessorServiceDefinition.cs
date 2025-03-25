namespace Connector.Company.v1;
using Connector.Company.v1.CompanyConfig;
using Connector.Company.v1.CompanyConfig.Update;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class CompanyV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<CompanyV1ActionProcessorConfig>
{
    public override string ModuleId => "company-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<CompanyV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<CompanyV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<CompanyV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<CompanyV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<CompanyV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<UpdateCompanyConfigHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, CompanyV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<UpdateCompanyConfigHandler, CompanyConfigDataObject>(ModuleId, "company-config", "update", config.UpdateCompanyConfigConfig);
    }
}