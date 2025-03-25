namespace Connector.Schedules.v1;
using Connector.Schedules.v1.ScenarioSchedules;
using Connector.Schedules.v1.ScenarioSchedulesv2;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class SchedulesV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<SchedulesV1CacheWriterConfig>
{
    public override string ModuleId => "schedules-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<SchedulesV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<SchedulesV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<SchedulesV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<SchedulesV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<SchedulesV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ScenarioSchedulesDataReader>();
        serviceCollection.AddSingleton<ScenarioSchedulesv2DataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ScenarioSchedulesDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ScenarioSchedulesv2DataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, SchedulesV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ScenarioSchedulesDataReader, ScenarioSchedulesDataObject>(ModuleId, config.ScenarioSchedulesConfig, dataReaderSettings);
        service.RegisterDataReader<ScenarioSchedulesv2DataReader, ScenarioSchedulesv2DataObject>(ModuleId, config.ScenarioSchedulesv2Config, dataReaderSettings);
    }
}