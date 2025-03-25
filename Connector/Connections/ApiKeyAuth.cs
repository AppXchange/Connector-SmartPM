using System;
using Xchange.Connector.SDK.Client.AuthTypes;
using Xchange.Connector.SDK.Client.ConnectionDefinitions.Attributes;
using System.Collections.Generic;

namespace Connector.Connections;

[ConnectionDefinition(title: "ApiKeyAuth", description: "SmartPM API Authentication")]
public class ApiKeyAuth : IApiKeyAuth
{
    [ConnectionProperty(title: "ApiKey", description: "SmartPM API Key generated from the Admin > Integrations section", isRequired: true, isSensitive: true)]
    public string ApiKey { get; init; } = string.Empty;

    [ConnectionProperty(title: "Connection Environment", description: "Select the environment to connect to", isRequired: true, isSensitive: false)]
    public ConnectionEnvironmentApiKeyAuth ConnectionEnvironment { get; set; } = ConnectionEnvironmentApiKeyAuth.Unknown;

    public string BaseUrl
    {
        get
        {
            switch (ConnectionEnvironment)
            {
                case ConnectionEnvironmentApiKeyAuth.Production:
                    return "https://live.smartpmtech.com";
                case ConnectionEnvironmentApiKeyAuth.Test:
                    return "https://test.smartpmtech.com";
                default:
                    throw new Exception("No base url was set.");
            }
        }
    }

    public IDictionary<string, string> GetHeaders(string companyId)
    {
        return new Dictionary<string, string>
        {
            { "X-API-KEY", ApiKey },
            { "X-COMPANY-ID", companyId }
        };
    }
}

public enum ConnectionEnvironmentApiKeyAuth
{
    Unknown = 0,
    Production = 1,
    Test = 2
}