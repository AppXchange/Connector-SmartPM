namespace Connector.User.v1;
using Connector.User.v1.CompanyUsers;
using Connector.User.v1.Users;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class UserV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<UserV1CacheWriterConfig>
{
    public override string ModuleId => "user-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<UserV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<UserV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<UserV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<UserV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<UserV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<UsersDataReader>();
        serviceCollection.AddSingleton<CompanyUsersDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<UsersDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<CompanyUsersDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, UserV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<UsersDataReader, UsersDataObject>(ModuleId, config.UsersConfig, dataReaderSettings);
        service.RegisterDataReader<CompanyUsersDataReader, CompanyUsersDataObject>(ModuleId, config.CompanyUsersConfig, dataReaderSettings);
    }
}