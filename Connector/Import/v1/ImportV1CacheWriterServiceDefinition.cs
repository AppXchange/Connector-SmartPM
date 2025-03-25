namespace Connector.Import.v1;
using Connector.Import.v1.ImportFile;
using Connector.Import.v1.ImportStatus;
using Connector.Import.v1.UploadFile;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ImportV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ImportV1CacheWriterConfig>
{
    public override string ModuleId => "import-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ImportV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ImportV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ImportV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ImportV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ImportV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ImportFileDataReader>();
        serviceCollection.AddSingleton<ImportStatusDataReader>();
        serviceCollection.AddSingleton<UploadFileDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ImportFileDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ImportStatusDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<UploadFileDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ImportV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ImportFileDataReader, ImportFileDataObject>(ModuleId, config.ImportFileConfig, dataReaderSettings);
        service.RegisterDataReader<ImportStatusDataReader, ImportStatusDataObject>(ModuleId, config.ImportStatusConfig, dataReaderSettings);
        service.RegisterDataReader<UploadFileDataReader, UploadFileDataObject>(ModuleId, config.UploadFileConfig, dataReaderSettings);
    }
}