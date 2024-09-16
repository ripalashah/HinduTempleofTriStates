using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.DataService;
using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intuit.Ipp.QueryFilter;

public class QuickBooksService
{
    private readonly OAuthService _oauthService;

    public QuickBooksService(OAuthService oauthService)
    {
        _oauthService = oauthService;
    }

    public async System.Threading.Tasks.Task SyncDonationToQuickBooksAsync(Donation donation)
    {
        try
        {
            // Fetch the OAuth tokens
            var tokens = await _oauthService.GetTokensAsync("AB11726446089dnLX4IRTL2LWfdirjgjT5b64PNvk6Ov2C5ZHW");
            if (tokens == null || string.IsNullOrEmpty(tokens.AccessToken))
            {
                throw new InvalidOperationException("Access token is missing or invalid.");
            }
            var oauthValidator = new OAuth2RequestValidator(tokens.AccessToken);
            var serviceContext = new ServiceContext("9341453104198392", IntuitServicesType.QBO, oauthValidator);
            var dataService = new DataService(serviceContext);

            // Query QuickBooks to get necessary entities (e.g., customer, item, etc.)
            var customer = new QueryService<Customer>(serviceContext).ExecuteIdsQuery("Select * From Customer StartPosition 1 MaxResults 1").FirstOrDefault();
            var item = new QueryService<Item>(serviceContext).ExecuteIdsQuery("Select * From Item StartPosition 1 MaxResults 1").FirstOrDefault();

            // Check if customer and item exist
            if (customer == null || item == null)
            {
                throw new Exception("Customer or Item not found in QuickBooks.");
            }
            // Create invoice for the donation
            var invoice = new Invoice
            {
                CustomerRef = new ReferenceType { Value = customer.Id },  // Customer reference ID in QuickBooks
                Line = new List<Line>
                {
                    new Line
                    {
                        Amount = (decimal)donation.Amount,  // Cast donation amount to decimal
                        Description = "Donation for temple",  // Add description
                        DetailType = LineDetailTypeEnum.SalesItemLineDetail,  // Correct DetailType for sales items
                        AnyIntuitObject = new SalesItemLineDetail  // Correctly assign the SalesItemLineDetail
                        {
                            ItemRef = new ReferenceType { Value = item.Id }  // Reference ID for the item in QuickBooks
                        }
                    }
                }.ToArray(),  // Convert List<Line> to Line[]
                TxnDate = DateTime.UtcNow,
                TotalAmt = (decimal)donation.Amount  // Set the total amount to match the donation amount
            };

            dataService.Add(invoice);
        }
        catch (Exception ex)
        {
            // Log error for debugging
            throw new ApplicationException("Error syncing donation to QuickBooks.", ex);
        }

    }

    public async System.Threading.Tasks.Task SyncLedgerToQuickBooksAsync(LedgerAccount ledgerAccount)
    {
        try
        {
            // Fetch OAuth tokens
            var tokens = await _oauthService.GetTokensAsync("AB11726446089dnLX4IRTL2LWfdirjgjT5b64PNvk6Ov2C5ZHW");
            var oauthValidator = new OAuth2RequestValidator(tokens.AccessToken);
            var serviceContext = new ServiceContext("9341453104198392", IntuitServicesType.QBO, oauthValidator);
            var dataService = new DataService(serviceContext);

            // Query QuickBooks to get necessary accounts (e.g., Accounts Receivable)
            var account = new QueryService<HinduTempleofTriStates.Models.LedgerAccount>(serviceContext).ExecuteIdsQuery("Select * From Account Where AccountType='Accounts Receivable' StartPosition 1 MaxResults 1").FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Accounts Receivable account not found in QuickBooks.");
            }
            // Create a journal entry for the ledger account
            var journalEntry = new JournalEntry
            {
                Line = new List<Line>
                {
                    new Line
                    {
                        Amount = (decimal)ledgerAccount.Balance,  // Ensure the balance is of type decimal
                        DetailType = LineDetailTypeEnum.JournalEntryLineDetail,  // Set the DetailType
                        AnyIntuitObject = new JournalEntryLineDetail  // Correctly assign the JournalEntryLineDetail
                        {
                            PostingType = PostingTypeEnum.Credit,  // Set to Credit
                            AccountRef = new ReferenceType { Value = account.Id.ToString() } // Convert Guid? to string for QuickBooks
                        }
                    }
                }.ToArray(),  // Convert List<Line> to Line[]
                TxnDate = DateTime.UtcNow  // Set the transaction date
            };

            // Add the journal entry to QuickBooks
            dataService.Add(journalEntry);
        }
        catch (Exception ex)
        {
            // Log error for debugging
            throw new ApplicationException("Error syncing ledger account to QuickBooks.", ex);
        }
    }
}
