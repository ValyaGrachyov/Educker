using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Persistence.services.GoogleService;

public class GoogleService: IGoogleService
{

    public async Task<GetTokenResult?> GetTokenAsync(string code, string appId, string appSecret, string redirectUri)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                const string url = "https://accounts.google.com/o/oauth2/token";
                object contentObject = new
                {
                    code,
                    client_id = appId,
                    client_secret = appSecret,
                    redirect_uri = redirectUri,
                    grant_type = "authorization_code"
                };
           
                var content = JsonContent.Create(contentObject);
                using var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = content;
                using var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetTokenResult>(responseText);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    
    public async Task<GetUserResult?> GetUserAsync(string accessToken, string tokenId)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var url = "https://www.googleapis.com/oauth2/v1/userinfo";
                url += "?alt=json";
                url += $"&access_token={accessToken}";

                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenId);
                using var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetUserResult>(responseText);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
    
public record GetTokenResult
{
    public string access_token { get; set; }
    public string id_token { get; set; }
}

public record GetUserResult
{
    public string email { get; set; }
    public string picture { get; set; }
}
