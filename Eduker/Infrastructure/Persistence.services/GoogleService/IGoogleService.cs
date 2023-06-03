namespace Persistence.services.GoogleService;

public interface IGoogleService
{
    public Task<GetTokenResult?> GetTokenAsync(string code, string appId, string appSecret,
        string redirectUri);

    public Task<GetUserResult?> GetUserAsync(string accessToken, string tokenId);
}