using HinduTempleofTriStates.Data;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class OAuthService
{
    private OAuth2Client _oauthClient;
    private readonly ILogger<OAuthService> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly QuickBooksSettingsService _quickBooksSettingsService;

    public OAuthService(QuickBooksSettingsService quickBooksSettingsService, ILogger<OAuthService> logger, ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _quickBooksSettingsService = quickBooksSettingsService;
        _logger = logger;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;

        // Fetch the QuickBooks settings from the database
        var quickBooksSettings = _quickBooksSettingsService.GetQuickBooksSettingsAsync().GetAwaiter().GetResult(); // Use async/await in real scenarios

        // Initialize OAuth2Client with settings fetched from the database
        _oauthClient = new OAuth2Client(
            quickBooksSettings.ClientId,
            quickBooksSettings.ClientSecret,
            quickBooksSettings.RedirectUrl,
            quickBooksSettings.Environment
        );
    }
    // Step 1: Redirect user to QuickBooks authorization page
    public IActionResult StartQuickBooksOAuth()
    {
        // Generate a random state to protect against CSRF attacks
        var state = Guid.NewGuid().ToString("N"); // Alternatively, you can use your `GenerateState()` method

        // Ensure that the session is available
        if (_httpContextAccessor.HttpContext?.Session == null)
        {
            _logger.LogError("Session or HttpContext is null.");
            return new BadRequestResult();
        }

        // Store the generated state in the session for validation during the callback
        _httpContextAccessor.HttpContext.Session.SetString("oauthState", state);

        // Specify required scopes for QuickBooks authorization
        List<OidcScopes> scopes = new List<OidcScopes> { OidcScopes.Accounting };

        // Generate the authorization URL using OAuth2Client
        string authorizationUrl = _oauthClient.GetAuthorizationURL(scopes, state);

        // Log the action for debugging purposes
        _logger.LogInformation("Redirecting to QuickBooks authorization URL: {AuthorizationUrl}", authorizationUrl);

        // Redirect the user to the generated authorization URL
        return new RedirectResult(authorizationUrl);
    }



    // Step 2: Handle callback from QuickBooks (extract authorization code and realmId)
    public async Task<IActionResult> QuickBooksCallback(string state, string code, string realmId)
    {
        if (_httpContextAccessor.HttpContext?.Session == null)
        {
            _logger.LogError("Session or HttpContext is null.");
            return new BadRequestResult();
        }

        // Retrieve and validate state stored in session
        var storedState = _httpContextAccessor.HttpContext.Session.GetString("oauthState");
        if (storedState != state)
        {
            _logger.LogError("State mismatch. Potential CSRF attack.");
            return new BadRequestResult();
        }

        // Step 3: Exchange authorization code for tokens
        var tokens = await ExchangeAuthorizationCodeForTokens(code, realmId);

        if (tokens == null)
        {
            return new StatusCodeResult(500); // Return appropriate error if token exchange fails
        }

        return new OkResult();  // Indicate success
    }

    // Step 3: Exchange authorization code for OAuth2 tokens
    public async Task<TokenResponse> ExchangeAuthorizationCodeForTokens(string authorizationCode, string realmId)
    {
        try
        {
            _logger.LogInformation("Exchanging authorization code for tokens...");

            // Retrieve tokens from QuickBooks using the authorization code
            var tokenResponse = await _oauthClient.GetBearerTokenAsync(authorizationCode);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("OAuth token retrieval failed.");
            }

            _logger.LogInformation("Tokens retrieved successfully.");

            // Save tokens to the database
            var oAuthToken = new OAuthToken
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.AccessTokenExpiresIn),
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(100),
                RealmId = realmId
            };

            // Save the tokens
            await SaveTokensAsync(oAuthToken);

            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exchanging authorization code for tokens.");
            throw new ApplicationException("Error exchanging authorization code for tokens.", ex);
        }
    }

    // Save tokens to the database
    public async Task SaveTokensAsync(OAuthToken oAuthToken)
    {
        try
        {
            // Add the new tokens to the database or update the existing record
            var existingToken = await _dbContext.OAuthTokens.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (existingToken == null)
            {
                _dbContext.OAuthTokens.Add(oAuthToken); // Add new tokens if none exist
            }
            else
            {
                // Update the existing tokens
                existingToken.AccessToken = oAuthToken.AccessToken;
                existingToken.RefreshToken = oAuthToken.RefreshToken;
                existingToken.AccessTokenExpiration = oAuthToken.AccessTokenExpiration;
                existingToken.RefreshTokenExpiration = oAuthToken.RefreshTokenExpiration;
                existingToken.RealmId = oAuthToken.RealmId;
            }
            _logger.LogInformation("Attempting to save OAuth tokens: AccessToken={AccessToken}, RefreshToken={RefreshToken}", oAuthToken.AccessToken, oAuthToken.RefreshToken);
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            _logger.LogInformation("Tokens saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving tokens to the database.");
            throw new ApplicationException("Error saving tokens.", ex);
        }
    }

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

            // Update stored tokens
            var tokenEntry = await _dbContext.OAuthTokens.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (tokenEntry != null)
            {
                tokenEntry.AccessToken = tokenResponse.AccessToken;
                tokenEntry.AccessTokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.AccessTokenExpiresIn);
                tokenEntry.RefreshToken = tokenResponse.RefreshToken;
                tokenEntry.RefreshTokenExpiration = DateTime.UtcNow.AddDays(100); // Adjust based on token validity
                await _dbContext.SaveChangesAsync();
            }

            return tokenResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing OAuth tokens.");
            throw new ApplicationException("Error refreshing OAuth tokens.", ex);
        }
    }

    public async Task<OAuthToken?> GetStoredTokensAsync()
    {
        try
        {
            var tokenEntry = await _dbContext.OAuthTokens
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            if (tokenEntry == null)
            {
                _logger.LogError("No stored tokens found in the database.");
            }
            else
            {
                _logger.LogInformation("Stored tokens retrieved successfully.");
            }

            return tokenEntry;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stored tokens from the database.");
            return null;
        }
    }

    public string GetAuthorizationUrl()
    {
        var state = Guid.NewGuid().ToString("N");
        if (_httpContextAccessor.HttpContext?.Session == null)
        {
            _logger.LogError("Session or HttpContext is null.");
            throw new InvalidOperationException("Session is not available.");
        }
        _httpContextAccessor.HttpContext.Session.SetString("oauthState", state);

        // Specify the QuickBooks Accounting scope
        List<OidcScopes> scopes = new List<OidcScopes> { OidcScopes.Accounting };

        if (_oauthClient == null)
        {
            _logger.LogError("OAuth client is not initialized.");
            throw new InvalidOperationException("OAuth client is not available.");
        }

        return _oauthClient.GetAuthorizationURL(scopes, state);
    }
}

