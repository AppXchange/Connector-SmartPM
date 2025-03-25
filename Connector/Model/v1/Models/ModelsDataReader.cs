using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Model.v1.Models;

public class ModelsResponse
{
    [JsonPropertyName("models")]
    public List<ModelsDataObject> Models { get; set; } = new();
}

public class ModelsDataReader : TypedAsyncDataReaderBase<ModelsDataObject>
{
    private readonly ILogger<ModelsDataReader> _logger;
    private readonly IApiClient _apiClient;
    private readonly string _projectId;

    public ModelsDataReader(
        ILogger<ModelsDataReader> logger,
        IApiClient apiClient,
        string projectId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _projectId = projectId;
    }

    public override async IAsyncEnumerable<ModelsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ModelsResponse? response = null;

        try
        {
            var apiResponse = await _apiClient.GetModelsAsync(
                _projectId,
                cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve models data. API StatusCode: {apiResponse.StatusCode}, Error: {apiResponse.ErrorMessage}");
            }

            response = apiResponse.GetData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving models data");
            throw;
        }

        if (response?.Models != null)
        {
            foreach (var model in response.Models)
            {
                yield return model;
            }
        }
    }
}