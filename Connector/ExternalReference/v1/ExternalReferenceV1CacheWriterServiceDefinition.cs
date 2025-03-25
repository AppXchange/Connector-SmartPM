namespace Connector.ExternalReference.v1;
using Connector.ExternalReference.v1.ExternalReference;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ExternalReferenceV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ExternalReferenceV1CacheWriterConfig>
{
    public override string ModuleId => "externalreference-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ExternalReferenceV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ExternalReferenceV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ExternalReferenceV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ExternalReferenceV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ExternalReferenceV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ExternalReferenceDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ExternalReferenceDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ExternalReferenceV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ExternalReferenceDataReader, ExternalReferenceDataObject>(ModuleId, config.ExternalReferenceConfig, dataReaderSettings);
    }
}