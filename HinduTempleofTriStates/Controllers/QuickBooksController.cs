using Microsoft.AspNetCore.Mvc;
using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Net;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using HinduTempleofTriStates.Services;

[Route("quickbooks")]
public class QuickBooksController : Controller
{
    private readonly OAuthService _oauthService;
    private readonly QuickBooksService _quickBooksService;
    private readonly IDonationService _donationService;
    private readonly LedgerService _ledgerService;
    public QuickBooksController(QuickBooksService quickBooksService, IDonationService donationService, OAuthService oauthService, LedgerService ledgerService)
    {
        _oauthService = oauthService;
        _quickBooksService = quickBooksService;
        _donationService = donationService;
        _ledgerService = ledgerService ?? throw new ArgumentNullException(nameof(ledgerService)); // Inject LedgerService
    }

    [HttpGet("connect")]
    public IActionResult Connect()
    {
        string authorizationUrl = _oauthService.GetAuthorizationUrl();
        return Redirect(authorizationUrl);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string AB117264579782W1kQnf2kf8EOdnUZZ1LSzGvTVpPNreXjAMCg)
    {
        if (!string.IsNullOrEmpty(AB117264579782W1kQnf2kf8EOdnUZZ1LSzGvTVpPNreXjAMCg))
        {
            var tokens = await _oauthService.GetTokensAsync(AB117264579782W1kQnf2kf8EOdnUZZ1LSzGvTVpPNreXjAMCg);
            // Save tokens in database for future use
            return RedirectToAction("QuickBooksSuccess");
        }
        return BadRequest("Failed to authenticate with QuickBooks.");
    }
    // Manually trigger donation sync to QuickBooks
    [HttpPost("sync-donation/{id}")]
    public async Task<IActionResult> SyncDonation(Guid id)
    {
        var donation = await _donationService.GetDonationByIdAsync(id);
        if (donation == null)
        {
            return NotFound();
        }

        await _quickBooksService.SyncDonationToQuickBooksAsync(donation);

        return Ok("Donation synced to QuickBooks successfully.");
    }

    // Manually trigger ledger account sync to QuickBooks
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
    public IActionResult QuickBooksSuccess()
    {
        return View();
    }
}
