namespace Connector.Projects.v1;
using Connector.Projects.v1.Project;
using Connector.Projects.v1.ProjectActivityCodeTypes;
using Connector.Projects.v1.ProjectCalendar;
using Connector.Projects.v1.ProjectCalendars;
using Connector.Projects.v1.ProjectComments;
using Connector.Projects.v1.Projects;
using Connector.Projects.v1.ProjectWorkBreakdownStructure;
using Connector.Projects.v1.ProjectWorkBreakdownStructureElement;
using Connector.Projects.v1.SpecificProjectActivityCodeType;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class ProjectsV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<ProjectsV1CacheWriterConfig>
{
    public override string ModuleId => "projects-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<ProjectsV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<ProjectsV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<ProjectsV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<ProjectsV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<ProjectsV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<ProjectsDataReader>();
        serviceCollection.AddSingleton<ProjectDataReader>();
        serviceCollection.AddSingleton<ProjectCommentsDataReader>();
        serviceCollection.AddSingleton<ProjectCalendarsDataReader>();
        serviceCollection.AddSingleton<ProjectCalendarDataReader>();
        serviceCollection.AddSingleton<ProjectWorkBreakdownStructureDataReader>();
        serviceCollection.AddSingleton<ProjectActivityCodeTypesDataReader>();
        serviceCollection.AddSingleton<SpecificProjectActivityCodeTypeDataReader>();
        serviceCollection.AddSingleton<ProjectWorkBreakdownStructureElementDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<ProjectsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectCommentsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectCalendarsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectCalendarDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectWorkBreakdownStructureDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectActivityCodeTypesDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<SpecificProjectActivityCodeTypeDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ProjectWorkBreakdownStructureElementDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, ProjectsV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<ProjectsDataReader, ProjectsDataObject>(ModuleId, config.ProjectsConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectDataReader, ProjectDataObject>(ModuleId, config.ProjectConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectCommentsDataReader, ProjectCommentsDataObject>(ModuleId, config.ProjectCommentsConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectCalendarsDataReader, ProjectCalendarsDataObject>(ModuleId, config.ProjectCalendarsConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectCalendarDataReader, ProjectCalendarDataObject>(ModuleId, config.ProjectCalendarConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectWorkBreakdownStructureDataReader, ProjectWorkBreakdownStructureDataObject>(ModuleId, config.ProjectWorkBreakdownStructureConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectActivityCodeTypesDataReader, ProjectActivityCodeTypesDataObject>(ModuleId, config.ProjectActivityCodeTypesConfig, dataReaderSettings);
        service.RegisterDataReader<SpecificProjectActivityCodeTypeDataReader, SpecificProjectActivityCodeTypeDataObject>(ModuleId, config.SpecificProjectActivityCodeTypeConfig, dataReaderSettings);
        service.RegisterDataReader<ProjectWorkBreakdownStructureElementDataReader, ProjectWorkBreakdownStructureElementDataObject>(ModuleId, config.ProjectWorkBreakdownStructureElementConfig, dataReaderSettings);
    }
}