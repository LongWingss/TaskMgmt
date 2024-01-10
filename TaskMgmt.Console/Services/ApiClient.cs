using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient httpClient;
    public readonly string BaseAddress;
    private bool _isAuthorized;

    public ApiClient(string baseAddress)
    {
        _isAuthorized = false;
        BaseAddress = baseAddress;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
    }
    public void SetBearerToken(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        _isAuthorized = true;
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        if (!_isAuthorized)
        {
            Console.WriteLine("Error: User is not authorized, token is invalid!");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        HttpResponseMessage response = await httpClient.GetAsync(endpoint);
        return response;
    }

    public async Task<HttpResponseMessage> GetAsyncToken(string endpoint)
    {
        if (!_isAuthorized)
        {
            Console.WriteLine("Error: User is not authorized, token is invalid!");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        HttpResponseMessage response = await httpClient.GetAsync(endpoint);
        return response;
    }
    public async Task<HttpResponseMessage> UserPostAsync(string endpoint, object data)
    {
        if (_isAuthorized)
        {
            Console.WriteLine("Error: User already authorized!");
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);

        if(!response.IsSuccessStatusCode)
            return new HttpResponseMessage(HttpStatusCode.BadRequest);

        string readToken = await response.Content.ReadAsStringAsync();
        SetBearerToken(readToken);

        return response;
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
    {
        if (!_isAuthorized)
        {
            Console.WriteLine("Error: User is not authorized, token is invalid!");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);
        return response;
    }

    //public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data)
    //{
    //    string jsonData = JsonSerializer.Serialize(data);
    //    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

    //    HttpResponseMessage response = await httpClient.PutAsync(endpoint, content);
    //    return await HandleResponse<T>(response);
    //}

    //public async Task<ApiResponse<T>> DeleteAsync<T>(string endpoint)
    //{
    //    HttpResponseMessage response = await httpClient.DeleteAsync(endpoint);
    //    return await HandleResponse<T>(response);
    //}

    //private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
    //{
    //    ApiResponse<T> apiResponse = new ApiResponse<T>();
    //    apiResponse.StatusCode = (int)response.StatusCode;
    //    if (response.IsSuccessStatusCode)
    //    {
    //        string content = await response.Content.ReadAsStringAsync();
    //        apiResponse.Result = JsonSerializer.Deserialize<T>(content);
    //    }
    //    else
    //    {
    //        throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode} - {response.ReasonPhrase}");
    //    }
    //    return apiResponse;
    //}
}