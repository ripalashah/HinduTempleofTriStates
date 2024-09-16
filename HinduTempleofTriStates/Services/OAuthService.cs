using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Configuration;

public class OAuthService
{
    private readonly IConfiguration _config;
    private OAuth2Client _oauthClient;
    private readonly ILogger<OAuthService> _logger;

    public OAuthService(IConfiguration config, ILogger<OAuthService> logger)
    {
        _config = config;
        _logger = logger;

        // Ensure that the configuration values are fetched correctly
        var clientId = _config["QuickBooks:ClientId"];
        var clientSecret = _config["QuickBooks:ClientSecret"];
        var redirectUri = _config["QuickBooks:RedirectUri"];
        var environment = _config["QuickBooks:Environment"];

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(redirectUri) || string.IsNullOrEmpty(environment))
        {
            throw new InvalidOperationException("QuickBooks OAuth configuration is missing or invalid.");
        }

        // Initialize OAuth2Client with configuration values
        _oauthClient = new OAuth2Client(clientId, clientSecret, redirectUri, environment);
    }
    // Method to retrieve OAuth tokens using the authorization code
    public async Task<TokenResponse> GetTokensAsync(string inputToken)
    {
        try
        {
            
            _logger.LogInformation("Fetching OAuth tokens...");
            TokenResponse tokenResponse;
            if (inputToken.StartsWith("AB117264579782W1kQnf2kf8EOdnUZZ1LSzGvTVpPNreXjAMCg"))
            { 
                tokenResponse = await _oauthClient.GetBearerTokenAsync(inputToken);
            }
            else
            {
                tokenResponse = await _oauthClient.GetBearerTokenAsync(inputToken);
            }

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("OAuth token retrieval failed.");
            }
            _logger.LogInformation("OAuth tokens retrieved successfully.");
            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving OAuth tokens.");
            throw new ApplicationException("Error retrieving OAuth tokens.", ex);
        }
    }

    // Method to refresh OAuth tokens using the refresh token
    public async Task<TokenResponse> RefreshTokensAsync(string refreshToken)
    {
        try
        {
            _logger.LogInformation("Refreshing OAuth tokens...");
            var tokenResponse = await _oauthClient.RefreshTokenAsync(refreshToken);
            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("Token refresh failed.");
            }
            _logger.LogInformation("OAuth tokens refreshed successfully.");
            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing OAuth tokens.");
            throw new ApplicationException("Error refreshing OAuth tokens.", ex);
        }
    }
    public string GetAuthorizationUrl()
    {
        // Use the correct OidcScopes enum or class values instead of strings
        List<OidcScopes> scopes = new List<OidcScopes>
    {
        OidcScopes.Accounting
    };

        return _oauthClient.GetAuthorizationURL(scopes);
    }

}
