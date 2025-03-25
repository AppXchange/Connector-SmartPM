using System.Net;

namespace Connector.Client;

/// <summary>
/// Base response class for API calls
/// </summary>
public class ApiResponse
{
    public bool IsSuccessful { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Generic response class for API calls with typed data
/// </summary>
/// <typeparam name="TResult">The type of data returned by the API</typeparam>
public sealed class ApiResponse<TResult> : ApiResponse
{
    private TResult? _data;

    public ApiResponse()
    {
    }

    public TResult? GetData() => _data;

    internal void SetData(TResult? value) => _data = value;
}