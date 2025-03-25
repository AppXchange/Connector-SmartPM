namespace Connector.ChangeLog.v1;
using Connector.ChangeLog.v1.AllChangeLogDetails;
using Connector.ChangeLog.v1.ChangeLogDetails;
using Connector.ChangeLog.v1.ChangeLogSummary;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ChangeLogV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ChangeLogV1CacheWriterConfig>
{
    public override string ModuleId => "changelog-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ChangeLogV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ChangeLogV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ChangeLogV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ChangeLogV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ChangeLogV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ChangeLogSummaryDataReader>();
        serviceCollection.AddSingleton<ChangeLogDetailsDataReader>();
        serviceCollection.AddSingleton<AllChangeLogDetailsDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ChangeLogSummaryDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ChangeLogDetailsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<AllChangeLogDetailsDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ChangeLogV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ChangeLogSummaryDataReader, ChangeLogSummaryDataObject>(ModuleId, config.ChangeLogSummaryConfig, dataReaderSettings);
        service.RegisterDataReader<ChangeLogDetailsDataReader, ChangeLogDetailsDataObject>(ModuleId, config.ChangeLogDetailsConfig, dataReaderSettings);
        service.RegisterDataReader<AllChangeLogDetailsDataReader, AllChangeLogDetailsDataObject>(ModuleId, config.AllChangeLogDetailsConfig, dataReaderSettings);
    }
}