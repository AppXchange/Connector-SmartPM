using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Connector.Connections;
using Connector.Projects.v1.Projects;
using Connector.Projects.v1.Project;
using Connector.Projects.v1.Project.Create;
using Connector.Projects.v1.ProjectCalendars;
using Connector.Projects.v1.ProjectCalendar;
using Connector.Projects.v1.ProjectWorkBreakdownStructure;
using Connector.Projects.v1.ProjectActivityCodeTypes;
using Connector.Projects.v1.SpecificProjectActivityCodeType;
using Connector.Projects.v1.ProjectWorkBreakdownStructureElement;
using Connector.Scenario.v1.Scenarios;
using Connector.Scenario.v1.ScenarioDetails;
using Connector.Scenario.v1.PrecentCompleteCurve;
using Connector.Scenario.v1.PrecentCompleteCurvev2;
using Connector.Scenario.v1.EarnedScheduleCurve;
using Connector.Scenario.v1.ScheduleCompression;
using Connector.Scenario.v1.ProjectHealth;
using Connector.Scenario.v1.ProjectHealthTrend;
using Connector.Scenario.v1.ScheduleCompressionTrend;
using Connector.Scenario.v1.SchedulePerformanceIndex;
using Connector.Scenario.v1.SchedulePerformanceIndexTrend;
using Connector.Activity.v1.Activities;
using Connector.Import.v1.ImportFile;
using Connector.Import.v1.ImportFile.Import;
using Connector.Import.v1.ImportStatus;
using Connector.Import.v1.UploadFile;
using Connector.User.v1.Users;
using Connector.User.v1.CompanyUsers;
using Connector.User.v1.CompanyUsers.Create;
using Connector.User.v1.CompanyUsers.Update;
using Connector.User.v1.CompanyUsers.Delete;
using System.Text.Json.Serialization;
using Connector.Client;
using System.IO;
using Connector.ExternalReference.v1.ExternalReference;
using System.Net;
using Connector.Schedules.v1.ScenarioSchedules;
using Connector.Schedules.v1.ScenarioSchedulesv2;
using Connector.Company.v1.CompanyConfig;
using Connector.ScheduleQuality.v1.ScheduleQuality;
using Connector.ScheduleQuality.v1.ScheduleQualityMetricDetails;
using Connector.ScheduleQuality.v1.AllQualityProfiles;
using Connector.ScheduleQuality.v1.QualityProfile;
using Connector.ChangeLog.v1.ChangeLogSummary;
using Connector.ChangeLog.v1.ChangeLogDetails;
using Connector.ChangeLog.v1.AllChangeLogDetails;
using Connector.Delay.v1.DelayTable;
using Connector.Reporting.v1.ShouldStartFinishReport;
using Connector.Reporting.v1.ShouldStartFinishTrend;
using Connector.Reporting.v1.MonthlyDistribution;
using Connector.CompanyMetrics.v1.CompanyHealthTrend;
using Connector.CompanyMetrics.v1.CompanyQualityTrend;
using Connector.CompanyMetrics.v1.CompanyCompressionTrend;
using Connector.CompanyMetrics.v1.CompanyMetricTrend;
using Connector.Model.v1.Models;

namespace Connector.Client;

/// <summary>
/// A client for interfacing with the API via the HTTP protocol.
/// </summary>
public interface IApiClient
{
    Task<ApiResponse<T>> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<object?>> TestConnection(CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectsDataObject>>> GetProjectsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProjectDataObject>> GetProjectByIdAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<CreateProjectActionOutput>> CreateProjectAsync(CreateProjectActionInput input, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteProjectAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<Dictionary<string, string>>> UpdateProjectMetadataAsync(string projectId, Dictionary<string, string> metadata, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectComment>>> GetProjectCommentsAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectComment>>> CreateProjectCommentAsync(string projectId, string notes, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectCalendarsDataObject>>> GetProjectCalendarsAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProjectCalendarDataObject>> GetProjectCalendarAsync(string projectId, string calendarId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectWorkBreakdownStructureDataObject>>> GetProjectWorkBreakdownStructureAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectActivityCodeTypesDataObject>>> GetProjectActivityCodeTypesAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<SpecificProjectActivityCodeTypeDataObject>> GetSpecificProjectActivityCodeTypeAsync(string projectId, string activityCodeTypeId, string activityCodeId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProjectWorkBreakdownStructureElementDataObject>> GetProjectWorkBreakdownStructureElementAsync(string projectId, string wbsId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ScenariosDataObject>>> GetProjectScenariosAsync(string projectId, string? asOf = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ScenarioDetailsDataObject>> GetScenarioDetailsAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<PrecentCompleteCurveDataObject>> GetPercentCompleteCurveAsync(string projectId, string scenarioId, CancellationToken cancellationToken = default);
    Task<ApiResponse<PrecentCompleteCurvev2DataObject>> GetPercentCompleteCurveV2Async(string projectId, string scenarioId, bool? delta = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<EarnedScheduleCurveDataObject>> GetEarnedScheduleCurveAsync(string projectId, string scenarioId, CancellationToken cancellationToken = default);
    Task<ApiResponse<ScheduleCompressionDataObject>> GetScheduleCompressionAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ProjectHealthDataObject>> GetProjectHealthAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ProjectHealthTrendDataObject>>> GetProjectHealthTrendAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ScheduleCompressionTrendDataObject>>> GetScheduleCompressionTrendAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<SchedulePerformanceIndexDataObject>> GetSchedulePerformanceIndexAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<SchedulePerformanceIndexTrendDataObject>>> GetSchedulePerformanceIndexTrendAsync(string projectId, string scenarioId, string? dataDate = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ActivitiesDataObject>>> GetActivitiesAsync(string projectId, string scenarioId, string? dataDate = null, string? filterId = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<ImportFileDataObject>> ImportFilesAsync(string projectId, List<ImportFileRequest> files, bool sendNotification, CancellationToken cancellationToken = default);
    Task<ApiResponse<ImportStatusDataObject>> GetImportStatusAsync(string projectId, string importId, CancellationToken cancellationToken = default);
    Task<ApiResponse<UploadFileDataObject>> UploadFileAsync(string fileName, Stream fileContent, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<UsersDataObject>>> GetProjectUsersAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<UsersDataObject>> AddProjectUserAsync(string projectId, string user, string role, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> RemoveProjectUserAsync(string projectId, string userId, CancellationToken cancellationToken);
    Task<ApiResponse<UsersDataObject>> UpdateProjectUserAsync(string projectId, string userId, string user, string role, CancellationToken cancellationToken);
    Task<ApiResponse<List<CompanyUsersDataObject>>> GetCompanyUsersAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<CreateCompanyUsersActionOutput>> CreateCompanyUserAsync(
        CreateCompanyUsersActionInput input,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<UpdateCompanyUsersActionOutput>> UpdateCompanyUserAsync(
        string userId,
        UpdateCompanyUsersActionInput input,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteCompanyUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ExternalReferenceDataObject>>> GetExternalReferencesAsync(string projectId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ExternalReferenceDataObject>>> AddExternalReferenceAsync(string projectId, string provider, string externalId, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ScenarioSchedulesDataObject>>> GetScenarioSchedulesAsync(string projectId, string scenarioId, string? asOf = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ScenarioSchedulesv2DataObject>>> GetScenarioSchedulesV2Async(string projectId, string scenarioId, string? asOf = null, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<CompanyConfigDataObject>>> GetCompanyConfigurationAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<CompanyConfigDataObject>> UpdateCompanyConfigurationAsync(string configuration, string setting, CancellationToken cancellationToken = default);
    Task<ApiResponse<ScheduleQualityDataObject>> GetScheduleQualityAsync(
        string projectId,
        string scenarioId,
        string? importLogId = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ScheduleQualityMetricDetailsDataObject>>> GetScheduleQualityMetricDetailsAsync(
        string projectId,
        string scenarioId,
        string metric,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<List<AllQualityProfilesDataObject>>> GetAllQualityProfilesAsync(
        CancellationToken cancellationToken = default);
    Task<ApiResponse<QualityProfileDataObject>> GetQualityProfileAsync(
        string qualityProfileId,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ChangeLogSummaryDataObject>>> GetChangeLogSummaryAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<List<ChangeLogDetailsDataObject>>> GetChangeLogDetailsAsync(
        string projectId,
        string scenarioId,
        string type,
        string? dataDate = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<List<AllChangeLogDetailsDataObject>>> GetAllChangeLogDetailsAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<List<DelayTableDataObject>>> GetDelayTableAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<ShouldStartFinishReportDataObject>> GetShouldStartFinishReportAsync(
        string projectId,
        string scenarioId,
        string startDate,
        string finishDate,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<HitRatesResponse>> GetShouldStartFinishTrendAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<VelocityResponse>> GetMonthlyDistributionAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<CompanyHealthTrendResponse>> GetCompanyHealthTrendAsync(
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<CompanyQualityTrendResponse>> GetCompanyQualityTrendAsync(
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<CompanyCompressionTrendResponse>> GetCompanyCompressionTrendAsync(
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<CompanyMetricTrendResponse>> GetCompanyMetricTrendAsync(
        string metricType,
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default);
    Task<ApiResponse<ModelsResponse>> GetModelsAsync(
        string projectId,
        CancellationToken cancellationToken = default);
}

public class ProjectComment
{
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; } = string.Empty;
}

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ApiKeyAuth _auth;
    private readonly ConnectorRegistrationConfig _config;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient(
        HttpClient httpClient,
        ApiKeyAuth auth,
        ConnectorRegistrationConfig config)
    {
        _httpClient = httpClient;
        _auth = auth;
        _config = config;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Configure base URL and default headers
        _httpClient.BaseAddress = new Uri(_auth.BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", _auth.ApiKey);
        _httpClient.DefaultRequestHeaders.Add("X-COMPANY-ID", _config.CompanyId);
    }

    public async Task<ApiResponse<T>> GetAsync<T>(
        string endpoint,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var builder = new UriBuilder($"{_httpClient.BaseAddress}{endpoint.TrimStart('/')}");

        if (queryParams != null && queryParams.Count > 0)
        {
            var query = string.Join("&", queryParams.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
            builder.Query = query;
        }

        try
        {
            var httpResponse = await _httpClient.GetAsync(builder.Uri, cancellationToken).ConfigureAwait(false);
            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<T>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<T>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<object?>> TestConnection(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient
                .GetAsync("public/v1/projects", cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return new ApiResponse<object?>
            {
                IsSuccessful = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                ErrorMessage = !response.IsSuccessStatusCode ? content : null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object?>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectsDataObject>>> GetProjectsAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        };

        return await GetAsync<List<ProjectsDataObject>>("public/v1/projects", queryParams, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ProjectDataObject>> GetProjectByIdAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<ProjectDataObject>($"public/v1/projects/{projectId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<CreateProjectActionOutput>> CreateProjectAsync(
        CreateProjectActionInput input,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var httpResponse = await _httpClient.PostAsJsonAsync("public/v1/projects", input, _jsonOptions, cancellationToken)
                .ConfigureAwait(false);
            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<CreateProjectActionOutput>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<CreateProjectActionOutput>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<CreateProjectActionOutput>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<object>> DeleteProjectAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"public/v1/projects/{projectId}", cancellationToken)
                .ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return new ApiResponse<object>
            {
                IsSuccessful = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                ErrorMessage = !response.IsSuccessStatusCode ? content : null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<Dictionary<string, string>>> UpdateProjectMetadataAsync(
        string projectId,
        Dictionary<string, string> metadata,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var httpResponse = await _httpClient.PutAsJsonAsync($"public/v1/projects/{projectId}/metadata", metadata, _jsonOptions, cancellationToken)
                .ConfigureAwait(false);
            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<Dictionary<string, string>>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<Dictionary<string, string>>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<Dictionary<string, string>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectComment>>> GetProjectCommentsAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await GetAsync<List<ProjectComment>>($"public/v1/projects/{projectId}/notes", cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response;
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectComment>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectComment>>> CreateProjectCommentAsync(
        string projectId,
        string notes,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new { notes = notes };
            var httpResponse = await _httpClient.PostAsJsonAsync($"public/v1/projects/{projectId}/notes", request, _jsonOptions, cancellationToken)
                .ConfigureAwait(false);
            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<List<ProjectComment>>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<List<ProjectComment>>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectComment>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectCalendarsDataObject>>> GetProjectCalendarsAsync(string projectId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<List<ProjectCalendarsDataObject>>($"public/v1/projects/{projectId}/calendars", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectCalendarsDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ProjectCalendarDataObject>> GetProjectCalendarAsync(
        string projectId,
        string calendarId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<ProjectCalendarDataObject>(
                $"public/v1/projects/{projectId}/calendars/{calendarId}",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProjectCalendarDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectWorkBreakdownStructureDataObject>>> GetProjectWorkBreakdownStructureAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<List<ProjectWorkBreakdownStructureDataObject>>(
                $"public/v1/projects/{projectId}/wbs",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectWorkBreakdownStructureDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectActivityCodeTypesDataObject>>> GetProjectActivityCodeTypesAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<List<ProjectActivityCodeTypesDataObject>>(
                $"public/v1/projects/{projectId}/activity-code-type",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectActivityCodeTypesDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<SpecificProjectActivityCodeTypeDataObject>> GetSpecificProjectActivityCodeTypeAsync(
        string projectId,
        string activityCodeTypeId,
        string activityCodeId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<SpecificProjectActivityCodeTypeDataObject>(
                $"public/v1/projects/{projectId}/activity-code-type/{activityCodeTypeId}/activity-codes/{activityCodeId}",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<SpecificProjectActivityCodeTypeDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ProjectWorkBreakdownStructureElementDataObject>> GetProjectWorkBreakdownStructureElementAsync(
        string projectId,
        string wbsId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<ProjectWorkBreakdownStructureElementDataObject>(
                $"public/v1/projects/{projectId}/wbs/{wbsId}",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProjectWorkBreakdownStructureElementDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ScenariosDataObject>>> GetProjectScenariosAsync(
        string projectId,
        string? asOf = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(asOf))
            {
                queryParams.Add("asOf", asOf);
            }

            return await GetAsync<List<ScenariosDataObject>>(
                $"public/v1/projects/{projectId}/scenarios",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ScenariosDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ScenarioDetailsDataObject>> GetScenarioDetailsAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<ScenarioDetailsDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ScenarioDetailsDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<PrecentCompleteCurveDataObject>> GetPercentCompleteCurveAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<PrecentCompleteCurveDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/percent-complete-curve",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<PrecentCompleteCurveDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<PrecentCompleteCurvev2DataObject>> GetPercentCompleteCurveV2Async(
        string projectId,
        string scenarioId,
        bool? delta = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (delta.HasValue)
            {
                queryParams.Add("delta", delta.Value.ToString().ToLower());
            }

            return await GetAsync<PrecentCompleteCurvev2DataObject>(
                $"public/v2/projects/{projectId}/scenarios/{scenarioId}/percent-complete-curve",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<PrecentCompleteCurvev2DataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<EarnedScheduleCurveDataObject>> GetEarnedScheduleCurveAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<EarnedScheduleCurveDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/earned-schedule-curve",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<EarnedScheduleCurveDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ScheduleCompressionDataObject>> GetScheduleCompressionAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<ScheduleCompressionDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedule-compression",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ScheduleCompressionDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ProjectHealthDataObject>> GetProjectHealthAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<ProjectHealthDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/project-health",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ProjectHealthDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ProjectHealthTrendDataObject>>> GetProjectHealthTrendAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<List<ProjectHealthTrendDataObject>>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/project-health-trend",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProjectHealthTrendDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ScheduleCompressionTrendDataObject>>> GetScheduleCompressionTrendAsync(
        string projectId,
        string scenarioId,
        string? dataDate,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                { "projectId", projectId },
                { "scenarioId", scenarioId }
            };

            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            var response = await GetAsync<List<ScheduleCompressionTrendDataObject>>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedule-compression-trend",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);

            return response;
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ScheduleCompressionTrendDataObject>>
            {
                IsSuccessful = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<SchedulePerformanceIndexDataObject>> GetSchedulePerformanceIndexAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<SchedulePerformanceIndexDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedule-performance-index",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<SchedulePerformanceIndexDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<SchedulePerformanceIndexTrendDataObject>>> GetSchedulePerformanceIndexTrendAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<List<SchedulePerformanceIndexTrendDataObject>>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedule-performance-index-trend",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<SchedulePerformanceIndexTrendDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ActivitiesDataObject>>> GetActivitiesAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        string? filterId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }
            if (!string.IsNullOrEmpty(filterId))
            {
                queryParams.Add("filterId", filterId);
            }

            return await GetAsync<List<ActivitiesDataObject>>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/activities",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ActivitiesDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ImportFileDataObject>> ImportFilesAsync(
        string projectId,
        List<ImportFileRequest> files,
        bool sendNotification,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new
            {
                files = files,
                sendNotification = sendNotification
            };

            var httpResponse = await _httpClient.PostAsJsonAsync(
                $"public/v1/projects/{projectId}/import",
                request,
                _jsonOptions,
                cancellationToken)
                .ConfigureAwait(false);

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<ImportFileDataObject>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<ImportFileDataObject>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<ImportFileDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ImportStatusDataObject>> GetImportStatusAsync(
        string projectId,
        string importId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<ImportStatusDataObject>(
                $"public/v1/projects/{projectId}/import/{importId}",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ImportStatusDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<UploadFileDataObject>> UploadFileAsync(
        string fileName,
        Stream fileContent,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var fileStreamContent = new StreamContent(fileContent);
            content.Add(fileStreamContent, "files", fileName);

            var httpResponse = await _httpClient.PostAsync(
                "public/v1/upload",
                content,
                cancellationToken)
                .ConfigureAwait(false);

            var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<UploadFileDataObject>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<UploadFileDataObject>(responseContent, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = responseContent;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<UploadFileDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<UsersDataObject>>> GetProjectUsersAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<List<UsersDataObject>>(
                $"public/v1/projects/{projectId}/users",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<UsersDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<UsersDataObject>> AddProjectUserAsync(
        string projectId,
        string user,
        string role,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new
            {
                user = user,
                role = role
            };

            var httpResponse = await _httpClient.PostAsJsonAsync(
                $"public/v1/projects/{projectId}/users",
                request,
                _jsonOptions,
                cancellationToken)
                .ConfigureAwait(false);

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<UsersDataObject>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<UsersDataObject>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<UsersDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<object>> RemoveProjectUserAsync(string projectId, string userId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"public/v1/projects/{projectId}/users/{userId}");
        return await SendAsync<object>(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<UsersDataObject>> UpdateProjectUserAsync(
        string projectId,
        string userId,
        string user,
        string role,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new
            {
                user = user,
                role = role
            };

            var httpResponse = await _httpClient.PutAsJsonAsync(
                $"public/v1/projects/{projectId}/users/{userId}",
                request,
                _jsonOptions,
                cancellationToken)
                .ConfigureAwait(false);

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<UsersDataObject>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<UsersDataObject>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<UsersDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<CompanyUsersDataObject>>> GetCompanyUsersAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync("public/v1/users", cancellationToken)
                .ConfigureAwait(false);

            var result = new ApiResponse<List<CompanyUsersDataObject>>();
            result.StatusCode = response.StatusCode;
            result.IsSuccessful = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<List<CompanyUsersDataObject>>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CompanyUsersDataObject>>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CreateCompanyUsersActionOutput>> CreateCompanyUserAsync(
        CreateCompanyUsersActionInput input,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("public/v1/users", input, cancellationToken)
                .ConfigureAwait(false);

            var result = new ApiResponse<CreateCompanyUsersActionOutput>();
            result.StatusCode = response.StatusCode;
            result.IsSuccessful = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<CreateCompanyUsersActionOutput>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<CreateCompanyUsersActionOutput>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<UpdateCompanyUsersActionOutput>> UpdateCompanyUserAsync(
        string userId,
        UpdateCompanyUsersActionInput input,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"public/v1/users/{userId}", input, _jsonOptions, cancellationToken)
                .ConfigureAwait(false);

            var result = new ApiResponse<UpdateCompanyUsersActionOutput>();
            result.StatusCode = response.StatusCode;
            result.IsSuccessful = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<UpdateCompanyUsersActionOutput>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<UpdateCompanyUsersActionOutput>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<object>> DeleteCompanyUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"public/v1/users/{userId}", cancellationToken)
                .ConfigureAwait(false);

            return new ApiResponse<object>
            {
                IsSuccessful = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                ErrorMessage = !response.IsSuccessStatusCode ? 
                    await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false) : 
                    null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ExternalReferenceDataObject>>> GetExternalReferencesAsync(string projectId, CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await _httpClient.GetAsync($"public/v1/projects/{projectId}/external-references", cancellationToken)
                .ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var data = JsonSerializer.Deserialize<List<ExternalReferenceDataObject>>(content, _jsonOptions);
                    var result = new ApiResponse<List<ExternalReferenceDataObject>>
                    {
                        IsSuccessful = true,
                        StatusCode = (HttpStatusCode)response.StatusCode,
                        ErrorMessage = null
                    };
                    result.SetData(data);
                    return result;
                }
                catch (JsonException ex)
                {
                    return new ApiResponse<List<ExternalReferenceDataObject>>
                    {
                        IsSuccessful = false,
                        StatusCode = (HttpStatusCode)response.StatusCode,
                        ErrorMessage = $"Failed to deserialize response: {ex.Message}"
                    };
                }
            }

            return new ApiResponse<List<ExternalReferenceDataObject>>
            {
                IsSuccessful = false,
                StatusCode = (HttpStatusCode)response.StatusCode,
                ErrorMessage = content
            };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<List<ExternalReferenceDataObject>>
            {
                IsSuccessful = false,
                StatusCode = (HttpStatusCode)(ex.StatusCode ?? HttpStatusCode.InternalServerError),
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ExternalReferenceDataObject>>> AddExternalReferenceAsync(
        string projectId,
        string provider,
        string externalId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new
            {
                provider = provider,
                externalId = externalId
            };

            var httpResponse = await _httpClient.PostAsJsonAsync(
                $"public/v1/projects/{projectId}/external-references",
                request,
                _jsonOptions,
                cancellationToken)
                .ConfigureAwait(false);

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);

            var result = new ApiResponse<List<ExternalReferenceDataObject>>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<List<ExternalReferenceDataObject>>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<List<ExternalReferenceDataObject>>
            {
                IsSuccessful = false,
                StatusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ScenarioSchedulesDataObject>>> GetScenarioSchedulesAsync(
        string projectId,
        string scenarioId,
        string? asOf = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(asOf))
            {
                queryParams.Add("asOf", asOf);
            }

            return await GetAsync<List<ScenarioSchedulesDataObject>>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedules",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<List<ScenarioSchedulesDataObject>>
            {
                IsSuccessful = false,
                StatusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<ScenarioSchedulesv2DataObject>>> GetScenarioSchedulesV2Async(
        string projectId,
        string scenarioId,
        string? asOf = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(asOf))
            {
                queryParams.Add("asOf", asOf);
            }

            return await GetAsync<List<ScenarioSchedulesv2DataObject>>(
                $"public/v2/projects/{projectId}/scenarios/{scenarioId}/schedules",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<List<ScenarioSchedulesv2DataObject>>
            {
                IsSuccessful = false,
                StatusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<CompanyConfigDataObject>>> GetCompanyConfigurationAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<List<CompanyConfigDataObject>>(
                "public/v1/configuration",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<List<CompanyConfigDataObject>>
            {
                IsSuccessful = false,
                StatusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CompanyConfigDataObject>> UpdateCompanyConfigurationAsync(
        string configuration,
        string setting,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new { setting = setting };
            var httpResponse = await _httpClient.PutAsJsonAsync(
                $"public/v1/configuration/{configuration}",
                request,
                _jsonOptions,
                cancellationToken)
                .ConfigureAwait(false);

            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<CompanyConfigDataObject>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<CompanyConfigDataObject>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<CompanyConfigDataObject>
            {
                IsSuccessful = false,
                StatusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ScheduleQualityDataObject>> GetScheduleQualityAsync(
        string projectId,
        string scenarioId,
        string? importLogId = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(importLogId))
        {
            queryParams.Add("importLogId", importLogId);
        }
        if (!string.IsNullOrEmpty(qualityProfileId))
        {
            queryParams.Add("qualityProfileId", qualityProfileId);
        }

        return await GetAsync<ScheduleQualityDataObject>(
            $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedule-quality",
            queryParams,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<ScheduleQualityMetricDetailsDataObject>>> GetScheduleQualityMetricDetailsAsync(
        string projectId,
        string scenarioId,
        string metric,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(qualityProfileId))
        {
            queryParams.Add("qualityProfileId", qualityProfileId);
        }

        return await GetAsync<List<ScheduleQualityMetricDetailsDataObject>>(
            $"public/v1/projects/{projectId}/scenarios/{scenarioId}/schedule-quality/{metric}/details",
            queryParams,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<AllQualityProfilesDataObject>>> GetAllQualityProfilesAsync(
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<List<AllQualityProfilesDataObject>>(
            "public/v1/quality-profiles",
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<QualityProfileDataObject>> GetQualityProfileAsync(
        string qualityProfileId,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<QualityProfileDataObject>(
            $"public/v1/quality-profiles/{qualityProfileId}",
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<ChangeLogSummaryDataObject>>> GetChangeLogSummaryAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<List<ChangeLogSummaryDataObject>>(
            $"public/v1/projects/{projectId}/scenarios/{scenarioId}/change-log-summary",
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<ChangeLogDetailsDataObject>>> GetChangeLogDetailsAsync(
        string projectId,
        string scenarioId,
        string type,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(dataDate))
        {
            queryParams.Add("dataDate", dataDate);
        }

        return await GetAsync<List<ChangeLogDetailsDataObject>>(
            $"public/v1/projects/{projectId}/scenarios/{scenarioId}/change-log/{type}",
            queryParams,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<AllChangeLogDetailsDataObject>>> GetAllChangeLogDetailsAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(dataDate))
        {
            queryParams.Add("dataDate", dataDate);
        }

        return await GetAsync<List<AllChangeLogDetailsDataObject>>(
            $"public/v1/projects/{projectId}/scenarios/{scenarioId}/change-log",
            queryParams,
            cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<List<DelayTableDataObject>>> GetDelayTableAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<List<DelayTableDataObject>>(
            $"public/v1/projects/{projectId}/scenarios/{scenarioId}/delay",
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ApiResponse<ShouldStartFinishReportDataObject>> GetShouldStartFinishReportAsync(
        string projectId,
        string scenarioId,
        string startDate,
        string finishDate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                { "startDate", startDate },
                { "finishDate", finishDate }
            };

            return await GetAsync<ShouldStartFinishReportDataObject>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/should-start-finish",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ShouldStartFinishReportDataObject>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<HitRatesResponse>> GetShouldStartFinishTrendAsync(
        string projectId,
        string scenarioId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<HitRatesResponse>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/should-start-finish-trend",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<HitRatesResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<VelocityResponse>> GetMonthlyDistributionAsync(
        string projectId,
        string scenarioId,
        string? dataDate = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataDate))
            {
                queryParams.Add("dataDate", dataDate);
            }

            return await GetAsync<VelocityResponse>(
                $"public/v1/projects/{projectId}/scenarios/{scenarioId}/velocity",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<VelocityResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CompanyHealthTrendResponse>> GetCompanyHealthTrendAsync(
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(periodType))
            {
                queryParams.Add("periodType", periodType);
            }
            if (!string.IsNullOrEmpty(filters))
            {
                queryParams.Add("filters", filters);
            }
            if (!string.IsNullOrEmpty(qualityProfileId))
            {
                queryParams.Add("qualityProfileId", qualityProfileId);
            }

            return await GetAsync<CompanyHealthTrendResponse>(
                $"public/v1/projects/company-health-trend",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CompanyHealthTrendResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CompanyQualityTrendResponse>> GetCompanyQualityTrendAsync(
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(periodType))
            {
                queryParams.Add("periodType", periodType);
            }
            if (!string.IsNullOrEmpty(filters))
            {
                queryParams.Add("filters", filters);
            }
            if (!string.IsNullOrEmpty(qualityProfileId))
            {
                queryParams.Add("qualityProfileId", qualityProfileId);
            }

            return await GetAsync<CompanyQualityTrendResponse>(
                "public/v1/company-trends/quality",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CompanyQualityTrendResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CompanyCompressionTrendResponse>> GetCompanyCompressionTrendAsync(
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(periodType))
            {
                queryParams.Add("periodType", periodType);
            }
            if (!string.IsNullOrEmpty(filters))
            {
                queryParams.Add("filters", filters);
            }
            if (!string.IsNullOrEmpty(qualityProfileId))
            {
                queryParams.Add("qualityProfileId", qualityProfileId);
            }

            return await GetAsync<CompanyCompressionTrendResponse>(
                "public/v1/company-trends/compression",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CompanyCompressionTrendResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<CompanyMetricTrendResponse>> GetCompanyMetricTrendAsync(
        string metricType,
        string? periodType = null,
        string? filters = null,
        string? qualityProfileId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(metricType))
            {
                queryParams.Add("metricType", metricType);
            }
            if (!string.IsNullOrEmpty(periodType))
            {
                queryParams.Add("periodType", periodType);
            }
            if (!string.IsNullOrEmpty(filters))
            {
                queryParams.Add("filters", filters);
            }
            if (!string.IsNullOrEmpty(qualityProfileId))
            {
                queryParams.Add("qualityProfileId", qualityProfileId);
            }

            return await GetAsync<CompanyMetricTrendResponse>(
                "public/v1/company-trends/metric",
                queryParams,
                cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CompanyMetricTrendResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<ModelsResponse>> GetModelsAsync(
        string projectId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetAsync<ModelsResponse>(
                $"public/v1/projects/{projectId}/models",
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ApiResponse<ModelsResponse>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }

    private async Task<ApiResponse<T>> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var httpResponse = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            var result = new ApiResponse<T>();
            result.StatusCode = httpResponse.StatusCode;
            result.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode && typeof(T) != typeof(object))
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<T>(content, _jsonOptions);
                    result.SetData(deserializedData);
                }
                catch (JsonException ex)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessage = $"Failed to deserialize response: {ex.Message}";
                }
            }
            else
            {
                result.ErrorMessage = content;
            }

            return result;
        }
        catch (Exception ex)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }
    }
}