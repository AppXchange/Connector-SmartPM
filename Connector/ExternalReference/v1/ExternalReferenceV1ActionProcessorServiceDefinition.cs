namespace Connector.ExternalReference.v1;
using Connector.ExternalReference.v1.ExternalReference;
using Connector.ExternalReference.v1.ExternalReference.Add;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ExternalReferenceV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ExternalReferenceV1ActionProcessorConfig>
{
    public override string ModuleId => "externalreference-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ExternalReferenceV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<ExternalReferenceV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ExternalReferenceV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ExternalReferenceV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ExternalReferenceV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<AddExternalReferenceHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, ExternalReferenceV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<AddExternalReferenceHandler, ExternalReferenceDataObject>(ModuleId, "external-reference", "add", config.AddExternalReferenceConfig);
    }
}