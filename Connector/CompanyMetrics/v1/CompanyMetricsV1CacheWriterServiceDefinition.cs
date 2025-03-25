namespace Connector.CompanyMetrics.v1;
using Connector.CompanyMetrics.v1.CompanyCompressionTrend;
using Connector.CompanyMetrics.v1.CompanyHealthTrend;
using Connector.CompanyMetrics.v1.CompanyMetricTrend;
using Connector.CompanyMetrics.v1.CompanyQualityTrend;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class CompanyMetricsV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<CompanyMetricsV1CacheWriterConfig>
{
    public override string ModuleId => "companymetrics-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<CompanyMetricsV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<CompanyMetricsV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<CompanyMetricsV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<CompanyMetricsV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<CompanyMetricsV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<CompanyHealthTrendDataReader>();
        serviceCollection.AddSingleton<CompanyQualityTrendDataReader>();
        serviceCollection.AddSingleton<CompanyCompressionTrendDataReader>();
        serviceCollection.AddSingleton<CompanyMetricTrendDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<CompanyHealthTrendDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<CompanyQualityTrendDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<CompanyCompressionTrendDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<CompanyMetricTrendDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, CompanyMetricsV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<CompanyHealthTrendDataReader, CompanyHealthTrendDataObject>(ModuleId, config.CompanyHealthTrendConfig, dataReaderSettings);
        service.RegisterDataReader<CompanyQualityTrendDataReader, CompanyQualityTrendDataObject>(ModuleId, config.CompanyQualityTrendConfig, dataReaderSettings);
        service.RegisterDataReader<CompanyCompressionTrendDataReader, CompanyCompressionTrendDataObject>(ModuleId, config.CompanyCompressionTrendConfig, dataReaderSettings);
        service.RegisterDataReader<CompanyMetricTrendDataReader, CompanyMetricTrendDataObject>(ModuleId, config.CompanyMetricTrendConfig, dataReaderSettings);
    }
}