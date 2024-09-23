using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Services;
using Intuit.Ipp.Security;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Newtonsoft.Json;
using static QuickBooksService;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Data;

[Route("quickbooks")]
public class QuickBooksController : Controller
{
    private readonly OAuthService _oauthService;
    private readonly QuickBooksService _quickBooksService;
    private readonly IDonationService _donationService;
    private readonly LedgerService _ledgerService;
    private readonly ILogger<QuickBooksController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public QuickBooksController(ApplicationDbContext context, QuickBooksService quickBooksService, IDonationService donationService, OAuthService oauthService, LedgerService ledgerService, ILogger<QuickBooksController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _oauthService = oauthService;
        _quickBooksService = quickBooksService;
        _donationService = donationService;
        _ledgerService = ledgerService ?? throw new ArgumentNullException(nameof(ledgerService)); // Inject LedgerService
        _logger = logger;  // Initialize ILogger here
        _httpContextAccessor = httpContextAccessor;  // Assign the injected IHttpContextAccessor
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("connect")]
    public IActionResult Connect()
    {
        // Directly use the IActionResult returned by the StartQuickBooksOAuth method
        return _oauthService.StartQuickBooksOAuth();
    }


    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string state, string code, string realmId)
    {
        // Verify that the state matches what was stored in the session
        var storedState = _httpContextAccessor.HttpContext?.Session.GetString("oauthState");
        if (storedState != state)
        {
            _logger.LogError("State mismatch. Potential CSRF attack.");
            return BadRequest("State validation failed.");
        }

        // Ensure realmId is available
        if (string.IsNullOrEmpty(realmId))
        {
            _logger.LogError("Realm ID is missing from the callback.");
            return BadRequest("Realm ID is missing.");
        }

        try
        {
            // Exchange the authorization code for tokens
            var tokens = await _oauthService.ExchangeAuthorizationCodeForTokens(code, realmId);
            if (tokens == null)
            {
                _logger.LogError("Failed to exchange authorization code for tokens.");
                return StatusCode(500, "Error retrieving tokens from QuickBooks.");
            }

            // Store the tokens and proceed with your application logic
            return RedirectToAction("QuickBooksSuccess"); // Or any other relevant action
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during QuickBooks authorization callback.");
            return StatusCode(500, "Error processing QuickBooks callback.");
        }
    }
    
    [HttpPost("sync-ledger/{id}")]
    public async Task<IActionResult> SyncLedger(Guid id)
    {
        var ledgerAccount = await _ledgerService.GetLedgerAccountByIdAsync(id);
        if (ledgerAccount == null)
        {
            return NotFound();
        }

        await _quickBooksService.SyncLedgerToQuickBooksAsync(ledgerAccount);

        return Ok("Ledger account synced to QuickBooks successfully.");
    }
}

