namespace Connector;
using Connector.Activity.v1;
using Connector.ChangeLog.v1;
using Connector.Client;
using Connector.Company.v1;
using Connector.CompanyMetrics.v1;
using Connector.Connections;
using Connector.Delay.v1;
using Connector.ExternalReference.v1;
using Connector.Import.v1;
using Connector.Model.v1;
using Connector.Projects.v1;
using Connector.Reporting.v1;
using Connector.Scenario.v1;
using Connector.ScheduleQuality.v1;
using Connector.Schedules.v1;
using Connector.User.v1;
using ESR.Hosting;
using ESR.Hosting.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Client.Testing;

/// <summary>
/// The registration object for your connector. This will be passed to the <see cref = "Extensions.UseGenericServiceRun"/> method at
/// Program startup. <see cref = "Program.Main(string[])"/>
/// </summary>
public class ConnectorRegistration : IConnectorRegistration<ConnectorRegistrationConfig>, IConfigureConnectorApiClient
{
    /// <summary>
    /// Registers any objects that are needed for dependency injection for the connector. 
    /// </summary>
    /// <param name = "serviceCollection"><see cref = "IServiceCollection"/> to register connector types with.</param>
    /// <param name = "hostContext">Host context that provides any configuration for the service run.</param>
    public void ConfigureServices(IServiceCollection serviceCollection, IHostContext hostContext)
    {
        var connectorRegistrationConfig = JsonSerializer.Deserialize<ConnectorRegistrationConfig>(hostContext.GetSystemConfig().Configuration);
        serviceCollection.AddSingleton(connectorRegistrationConfig!);
        serviceCollection.AddTransient<RetryPolicyHandler>();
    }

    /// <summary>
    /// Registers all <see cref = "IConnectorServiceDefinition"/> implementations. If using the xchange CLI tooling, it will normally
    /// add these for you when adding a new module to the connector. Most modules will have an Action processor service definition
    /// and a Cache Writer service definition
    /// </summary>
    /// <param name = "serviceCollection"></param>
    public void RegisterServiceDefinitions(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ProjectsV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ProjectsV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ScenarioV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ScenarioV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ActivityV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ActivityV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ImportV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ImportV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, UserV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, UserV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ExternalReferenceV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ExternalReferenceV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, SchedulesV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, SchedulesV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, CompanyV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, CompanyV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ScheduleQualityV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ScheduleQualityV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ChangeLogV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ChangeLogV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, DelayV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, DelayV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ReportingV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ReportingV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, CompanyMetricsV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, CompanyMetricsV1CacheWriterServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ModelV1ActionProcessorServiceDefinition>();
        serviceCollection.AddSingleton<IConnectorServiceDefinition, ModelV1CacheWriterServiceDefinition>();
    }

    public void ConfigureConnectorApiClient(IServiceCollection serviceCollection, IHostConnectionContext hostConnectionContext)
    {
        var activeConnection = hostConnectionContext.GetConnection();
        serviceCollection.ResolveServices(activeConnection);
    }

    public void RegisterConnectionTestHandler(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConnectionTestHandler, ConnectionTestHandler>();
    }
}