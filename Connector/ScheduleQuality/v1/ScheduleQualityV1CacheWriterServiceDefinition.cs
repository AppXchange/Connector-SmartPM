namespace Connector.ScheduleQuality.v1;
using Connector.ScheduleQuality.v1.AllQualityProfiles;
using Connector.ScheduleQuality.v1.QualityProfile;
using Connector.ScheduleQuality.v1.ScheduleQuality;
using Connector.ScheduleQuality.v1.ScheduleQualityMetricDetails;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ScheduleQualityV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ScheduleQualityV1CacheWriterConfig>
{
    public override string ModuleId => "schedulequality-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ScheduleQualityV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ScheduleQualityV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ScheduleQualityV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ScheduleQualityV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ScheduleQualityV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ScheduleQualityDataReader>();
        serviceCollection.AddSingleton<ScheduleQualityMetricDetailsDataReader>();
        serviceCollection.AddSingleton<AllQualityProfilesDataReader>();
        serviceCollection.AddSingleton<QualityProfileDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ScheduleQualityDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ScheduleQualityMetricDetailsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<AllQualityProfilesDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<QualityProfileDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ScheduleQualityV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ScheduleQualityDataReader, ScheduleQualityDataObject>(ModuleId, config.ScheduleQualityConfig, dataReaderSettings);
        service.RegisterDataReader<ScheduleQualityMetricDetailsDataReader, ScheduleQualityMetricDetailsDataObject>(ModuleId, config.ScheduleQualityMetricDetailsConfig, dataReaderSettings);
        service.RegisterDataReader<AllQualityProfilesDataReader, AllQualityProfilesDataObject>(ModuleId, config.AllQualityProfilesConfig, dataReaderSettings);
        service.RegisterDataReader<QualityProfileDataReader, QualityProfileDataObject>(ModuleId, config.QualityProfileConfig, dataReaderSettings);
    }
}