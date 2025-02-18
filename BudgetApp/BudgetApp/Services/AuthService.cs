using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "http://localhost:5000/api/";

    public AuthService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(ApiBaseUrl) };
    }

    public async Task<HttpResponseMessage> RegisterAsync(string username, string email, string password, string companyName, string companyAddress)
    {
        var data = new
        {
            username,
            email,
            password,
            companyName,
            companyAddress
        };

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync("auth/register", content);
    }

    public async Task<HttpResponseMessage> LoginAsync(string username, string password)
    {
        var data = new
        {
            username,
            password
        };

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync("auth/login", content);
    }
}