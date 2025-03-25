namespace Connector.Import.v1;
using Connector.Import.v1.ImportFile;
using Connector.Import.v1.ImportFile.Import;
using Connector.Import.v1.UploadFile;
using Connector.Import.v1.UploadFile.Upload;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class ImportV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<ImportV1ActionProcessorConfig>
{
    public override string ModuleId => "import-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<ImportV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<ImportV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<ImportV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<ImportV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<ImportV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<ImportImportFileHandler>();
        serviceCollection.AddScoped<UploadUploadFileHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, ImportV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<ImportImportFileHandler, ImportFileDataObject>(ModuleId, "import-file", "import", config.ImportImportFileConfig);
        service.RegisterHandlerForDataObjectAction<UploadUploadFileHandler, UploadFileDataObject>(ModuleId, "upload-file", "upload", config.UploadUploadFileConfig);
    }
}