namespace Connector.Scenario.v1;
using Connector.Scenario.v1.EarnedScheduleCurve;
using Connector.Scenario.v1.PrecentCompleteCurve;
using Connector.Scenario.v1.PrecentCompleteCurvev2;
using Connector.Scenario.v1.ProjectHealth;
using Connector.Scenario.v1.ProjectHealthTrend;
using Connector.Scenario.v1.ScenarioDetails;
using Connector.Scenario.v1.Scenarios;
using Connector.Scenario.v1.ScheduleCompression;
using Connector.Scenario.v1.ScheduleCompressionTrend;
using Connector.Scenario.v1.SchedulePerformanceIndex;
using Connector.Scenario.v1.SchedulePerformanceIndexTrend;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ScenarioV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ScenarioV1CacheWriterConfig>
{
    public override string ModuleId => "scenario-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ScenarioV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ScenarioV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ScenarioV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ScenarioV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ScenarioV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ScenariosDataReader>();
        serviceCollection.AddSingleton<ScenarioDetailsDataReader>();
        serviceCollection.AddSingleton<PrecentCompleteCurveDataReader>();
        serviceCollection.AddSingleton<PrecentCompleteCurvev2DataReader>();
        serviceCollection.AddSingleton<EarnedScheduleCurveDataReader>();
        serviceCollection.AddSingleton<ScheduleCompressionDataReader>();
        serviceCollection.AddSingleton<ProjectHealthDataReader>();
        serviceCollection.AddSingleton<ProjectHealthTrendDataReader>();
        serviceCollection.AddSingleton<ScheduleCompressionTrendDataReader>();
        serviceCollection.AddSingleton<SchedulePerformanceIndexDataReader>();
        serviceCollection.AddSingleton<SchedulePerformanceIndexTrendDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ScenariosDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ScenarioDetailsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<PrecentCompleteCurveDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<PrecentCompleteCurvev2DataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<EarnedScheduleCurveDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ScheduleCompressionDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectHealthDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectHealthTrendDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ScheduleCompressionTrendDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<SchedulePerformanceIndexDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<SchedulePerformanceIndexTrendDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ScenarioV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ScenariosDataReader, ScenariosDataObject>(ModuleId, config.ScenariosConfig, dataReaderSettings);
        service.RegisterDataReader<ScenarioDetailsDataReader, ScenarioDetailsDataObject>(ModuleId, config.ScenarioDetailsConfig, dataReaderSettings);
        service.RegisterDataReader<PrecentCompleteCurveDataReader, PrecentCompleteCurveDataObject>(ModuleId, config.PrecentCompleteCurveConfig, dataReaderSettings);
        service.RegisterDataReader<PrecentCompleteCurvev2DataReader, PrecentCompleteCurvev2DataObject>(ModuleId, config.PrecentCompleteCurvev2Config, dataReaderSettings);
        service.RegisterDataReader<EarnedScheduleCurveDataReader, EarnedScheduleCurveDataObject>(ModuleId, config.EarnedScheduleCurveConfig, dataReaderSettings);
        service.RegisterDataReader<ScheduleCompressionDataReader, ScheduleCompressionDataObject>(ModuleId, config.ScheduleCompressionConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectHealthDataReader, ProjectHealthDataObject>(ModuleId, config.ProjectHealthConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectHealthTrendDataReader, ProjectHealthTrendDataObject>(ModuleId, config.ProjectHealthTrendConfig, dataReaderSettings);
        service.RegisterDataReader<ScheduleCompressionTrendDataReader, ScheduleCompressionTrendDataObject>(ModuleId, config.ScheduleCompressionTrendConfig, dataReaderSettings);
        service.RegisterDataReader<SchedulePerformanceIndexDataReader, SchedulePerformanceIndexDataObject>(ModuleId, config.SchedulePerformanceIndexConfig, dataReaderSettings);
        service.RegisterDataReader<SchedulePerformanceIndexTrendDataReader, SchedulePerformanceIndexTrendDataObject>(ModuleId, config.SchedulePerformanceIndexTrendConfig, dataReaderSettings);
    }
}