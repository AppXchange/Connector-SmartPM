namespace Connector.Activity.v1;
using Connector.Activity.v1.Activities;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ActivityV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ActivityV1CacheWriterConfig>
{
    public override string ModuleId => "activity-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ActivityV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ActivityV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ActivityV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ActivityV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ActivityV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ActivitiesDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ActivitiesDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ActivityV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ActivitiesDataReader, ActivitiesDataObject>(ModuleId, config.ActivitiesConfig, dataReaderSettings);
    }
}