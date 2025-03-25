namespace Connector.Reporting.v1;
using Connector.Reporting.v1.MonthlyDistribution;
using Connector.Reporting.v1.ShouldStartFinishReport;
using Connector.Reporting.v1.ShouldStartFinishTrend;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ReportingV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ReportingV1CacheWriterConfig>
{
    public override string ModuleId => "reporting-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ReportingV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ReportingV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ReportingV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ReportingV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ReportingV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ShouldStartFinishReportDataReader>();
        serviceCollection.AddSingleton<ShouldStartFinishTrendDataReader>();
        serviceCollection.AddSingleton<MonthlyDistributionDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ShouldStartFinishReportDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ShouldStartFinishTrendDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<MonthlyDistributionDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ReportingV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ShouldStartFinishReportDataReader, ShouldStartFinishReportDataObject>(ModuleId, config.ShouldStartFinishReportConfig, dataReaderSettings);
        service.RegisterDataReader<ShouldStartFinishTrendDataReader, ShouldStartFinishTrendDataObject>(ModuleId, config.ShouldStartFinishTrendConfig, dataReaderSettings);
        service.RegisterDataReader<MonthlyDistributionDataReader, MonthlyDistributionDataObject>(ModuleId, config.MonthlyDistributionConfig, dataReaderSettings);
    }
}