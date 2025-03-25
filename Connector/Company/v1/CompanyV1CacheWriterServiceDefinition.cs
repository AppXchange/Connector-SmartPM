namespace Connector.Company.v1;
using Connector.Company.v1.CompanyConfig;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class CompanyV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<CompanyV1CacheWriterConfig>
{
    public override string ModuleId => "company-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<CompanyV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<CompanyV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<CompanyV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<CompanyV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<CompanyV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<CompanyConfigDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<CompanyConfigDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, CompanyV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<CompanyConfigDataReader, CompanyConfigDataObject>(ModuleId, config.CompanyConfigConfig, dataReaderSettings);
    }
}