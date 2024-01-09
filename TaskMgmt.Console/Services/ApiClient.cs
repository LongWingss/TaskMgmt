using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient httpClient;

    public ApiClient(string baseAddress)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        HttpResponseMessage response = await httpClient.GetAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);
        return await HandleResponse<T>(response);
    }

    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PutAsync(endpoint, content);
        return await HandleResponse<T>(response);
    }

    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        HttpResponseMessage response = await httpClient.DeleteAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }
        else
        {
            throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
}
