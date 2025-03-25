namespace Connector.Projects.v1;
using Connector.Projects.v1.Project;
using Connector.Projects.v1.Project.Create;
using Connector.Projects.v1.Project.Delete;
using Connector.Projects.v1.Project.UpdateMetadata;
using Connector.Projects.v1.ProjectComments;
using Connector.Projects.v1.ProjectComments.Create;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ProjectsV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ProjectsV1ActionProcessorConfig>
{
    public override string ModuleId => "projects-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ProjectsV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<ProjectsV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ProjectsV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ProjectsV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ProjectsV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<CreateProjectHandler>();
        serviceCollection.AddScoped<DeleteProjectHandler>();
        serviceCollection.AddScoped<UpdateMetadataProjectHandler>();
        serviceCollection.AddScoped<CreateProjectCommentsHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, ProjectsV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<CreateProjectHandler, ProjectDataObject>(ModuleId, "project", "create", config.CreateProjectConfig);
        service.RegisterHandlerForDataObjectAction<DeleteProjectHandler, ProjectDataObject>(ModuleId, "project", "delete", config.DeleteProjectConfig);
        service.RegisterHandlerForDataObjectAction<UpdateMetadataProjectHandler, ProjectDataObject>(ModuleId, "project", "update-metadata", config.UpdateMetadataProjectConfig);
        service.RegisterHandlerForDataObjectAction<CreateProjectCommentsHandler, ProjectCommentsDataObject>(ModuleId, "project-comments", "create", config.CreateProjectCommentsConfig);
    }
}