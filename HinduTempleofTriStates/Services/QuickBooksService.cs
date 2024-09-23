using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.DataService;
using HinduTempleofTriStates.Models;
using Intuit.Ipp.QueryFilter;
using Task = Intuit.Ipp.Data.Task;
using Intuit.Ipp.Exception;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class QuickBooksService
{
    private readonly OAuthService _oauthService;
    private readonly ILogger<QuickBooksService> _logger;

    public QuickBooksService(OAuthService oauthService, ILogger<QuickBooksService> logger)
    {
        _oauthService = oauthService;
        _logger = logger;
    }

    public async IAsyncEnumerable<string> SyncDonationToQuickBooksAsync(Donation donation)
    {
        var statusMessages = new List<string>();
        bool shouldRetry = false; // Flag to indicate whether to retry the operation

        try
        {
            // Step 1: Fetch stored OAuth tokens
            var storedTokens = await _oauthService.GetStoredTokensAsync();
            if (storedTokens == null || string.IsNullOrEmpty(storedTokens.AccessToken))
            {
                _logger.LogError("No stored tokens found. Unable to proceed with the QuickBooks sync.");
                statusMessages.Add("Stored tokens are missing. Please re-authenticate.");
                yield break; // Stop the method early if tokens are missing
            }
            statusMessages.Add("OAuth tokens retrieved successfully.");

            // Step 2: Create OAuth validator and service context
            var oauthValidator = new OAuth2RequestValidator(storedTokens.AccessToken);
            string realmId = storedTokens.RealmId; // Use the actual realm ID from stored tokens
            var serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            var dataService = new DataService(serviceContext);
            statusMessages.Add("Service context created successfully.");

            // Step 3: Verify the income account exists in QuickBooks
            var queryService = new QueryService<Intuit.Ipp.Data.Account>(serviceContext);
            var account = queryService.ExecuteIdsQuery("SELECT * FROM Account WHERE AccountType = 'Income' AND Name = 'Donations'").FirstOrDefault();

            if (account == null)
            {
                // If account does not exist, create a new account
                var newAccount = new Intuit.Ipp.Data.Account
                {
                    Name = "Donations",
                    AccountType = Intuit.Ipp.Data.AccountTypeEnum.Income, // Specify the correct namespace
                    AccountSubType = "SalesOfProductIncome", // Adjust based on your needs
                    Classification = Intuit.Ipp.Data.AccountClassificationEnum.Revenue, // Correct the enum usage
                    CurrencyRef = new ReferenceType
                    {
                        Value = "USD" // Currency code, value needed only
                    },
                    FullyQualifiedName = "Donations"
                };

                try
                {
                    account = dataService.Add(newAccount);
                    statusMessages.Add("Income account 'Donations' created successfully in QuickBooks.");
                    _logger.LogInformation("Income account 'Donations' created successfully.");
                }
                catch (IdsException idsEx)
                {
                    _logger.LogError(idsEx, "Failed to create income account in QuickBooks.");
                    statusMessages.Add($"Failed to create income account: {idsEx.Message}");
                    yield break; // Stop if the account creation fails
                }
            }
            else
            {
                statusMessages.Add("Income account verified successfully.");
            }

            // Step 4: Create an invoice for the donation using the data from the Donation model
            var invoice = new Invoice
            {
                TxnDate = donation.Date,
                PrivateNote = $"Donation Category: {donation.DonationCategory}, Type: {donation.DonationType}",
                Line = new List<Line> // Create the list of lines
                {
                    new Line
                    {
                        Amount = (decimal)donation.Amount,
                        DetailType = LineDetailTypeEnum.SalesItemLineDetail, // Correct DetailType
                        Description = $"Donation from {donation.DonorName}", // Set a simple description
                        AnyIntuitObject = new SalesItemLineDetail // Correctly set SalesItemLineDetail using AnyIntuitObject
                        {
                            ItemRef = new ReferenceType
                            {
                                Value = "1" // Replace with actual item reference in QuickBooks
                            },
                            AnyIntuitObject = (decimal)donation.Amount, // Set unit price here
                            ItemElementName = ItemChoiceType.UnitPrice, // Specify that this value represents the unit price
                            Qty = 1, // Set quantity as needed
                            TaxCodeRef = new ReferenceType
                            {
                                Value = "NON" // Adjust based on your tax code
                            }
                        }
                    }
                }.ToArray(), // Convert the List<Line> to an array before assigning
                CustomerRef = new ReferenceType
                {
                    Value = "1" // Replace with the actual customer reference in QuickBooks
                },
                BillAddr = new PhysicalAddress
                {
                    Line1 = $"{donation.City}, {donation.State}",
                    Country = donation.Country
                },
                SalesTermRef = new ReferenceType
                {
                    Value = "DueOnReceipt" // Set terms as required
                }
            };

            // Step 5: Add the invoice to QuickBooks asynchronously
            try
            {
                await System.Threading.Tasks.Task.Run(() => dataService.Add(invoice)); // Ensure proper namespace for Task.Run
                statusMessages.Add("Invoice added to QuickBooks successfully.");
                _logger.LogInformation("Donation synced successfully with QuickBooks.");
            }
            catch (IdsException idsEx) when (idsEx.Message.Contains("Forbidden"))
            {
                _logger.LogError(idsEx, "Access forbidden: Check permissions, scopes, and endpoint configuration.");
                statusMessages.Add("403 Forbidden: Check permissions and configuration.");
            }
        }
        catch (InvalidTokenException ex)
        {
            _logger.LogError(ex, "Invalid token detected. Attempting to refresh tokens.");
            statusMessages.Add("Invalid token. Attempting to refresh tokens...");

            // Attempt to refresh tokens
            var tokenRefreshed = await RefreshAccessTokenAsync();
            if (!tokenRefreshed)
            {
                statusMessages.Add("Failed to refresh tokens. Please re-authenticate.");
            }
            else
            {
                shouldRetry = true; // Set flag to retry the sync operation
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error syncing donation to QuickBooks.");
            statusMessages.Add($"Error syncing donation: {ex.Message}");
        }

        // Yield the collected status messages
        foreach (var message in statusMessages)
        {
            yield return message;
        }

        // Retry the operation if flagged for retry after catching InvalidTokenException
        if (shouldRetry)
        {
            _logger.LogInformation("Retrying SyncDonationToQuickBooksAsync after refreshing tokens.");
            await foreach (var retryMessage in SyncDonationToQuickBooksAsync(donation))
            {
                yield return retryMessage; // Yield messages from the retry operation
            }
        }
    }



    //Mapping method to convert TokenResponse to OAuthToken
    private OAuthToken MapTokenResponseToOAuthToken(TokenResponse tokenResponse, OAuthToken existingToken)
    {
        // Assuming OAuthToken is your model class with the properties below
        return new OAuthToken
        {
            Id = existingToken?.Id ?? Guid.NewGuid(), // Use existing ID if available, otherwise create a new one
            AccessToken = tokenResponse.AccessToken, // Assign the new access token from TokenResponse
            RefreshToken = tokenResponse.RefreshToken, // Assign the new refresh token from TokenResponse
            AccessTokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.AccessTokenExpiresIn), // Set the access token expiration based on the expiresIn value from the TokenResponse
            RefreshTokenExpiration = DateTime.UtcNow.AddDays(180), // Assuming refresh tokens expire in 180 days; adjust this value based on your actual expiration policy
            RealmId = existingToken?.RealmId ?? string.Empty// Maintain the existing RealmId if it's already stored; otherwise, this would remain unchanged
        };
    }

    public async Task<bool> RefreshAccessTokenAsync()
    {
        try
        {
            // Retrieve the current stored tokens
            var storedTokens = await _oauthService.GetStoredTokensAsync();

            if (storedTokens == null || string.IsNullOrEmpty(storedTokens.RefreshToken))
            {
                _logger.LogError("No valid refresh token found. Please re-authenticate.");
                return false;
            }

            // Request new tokens using the refresh token
            var newTokenResponse = await _oauthService.RefreshTokensAsync(storedTokens.RefreshToken);

            if (newTokenResponse != null)
            {
                // Map TokenResponse to your OAuthToken model
                var updatedTokens = MapTokenResponseToOAuthToken(newTokenResponse, storedTokens);

                // Save the new tokens for future requests
                await _oauthService.SaveTokensAsync(updatedTokens);
                _logger.LogInformation("Access token successfully refreshed.");
                return true;
            }
            else
            {
                _logger.LogError("Failed to refresh access token. Please check your OAuth configuration.");
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while refreshing tokens.");
            return false;
        }
    }

    private async System.Threading.Tasks.Task HandleTokenRefreshAsync(OAuthToken storedTokens)
    {
        try
        {
            if (string.IsNullOrEmpty(storedTokens.RefreshToken) || DateTime.UtcNow >= storedTokens.RefreshTokenExpiration)
            {
                _logger.LogError("Refresh token is missing or expired. Unable to refresh access token.");
                throw new MissingOAuthTokensException("Refresh token is missing or expired. Please re-authenticate.");
            }

            _logger.LogInformation("Attempting to refresh access token using refresh token.");
            var tokenResponse = await _oauthService.RefreshTokensAsync(storedTokens.RefreshToken);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                _logger.LogError("Failed to refresh access token.");
                throw new MissingOAuthTokensException("Access token is missing or invalid. Please re-authenticate.");
            }

            storedTokens.AccessToken = tokenResponse.AccessToken;
            storedTokens.AccessTokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.AccessTokenExpiresIn);
            storedTokens.RefreshToken = tokenResponse.RefreshToken;
            storedTokens.RefreshTokenExpiration = DateTime.UtcNow.AddDays(100);

            await _oauthService.SaveTokensAsync(storedTokens);
            _logger.LogInformation("Tokens refreshed and saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token refresh process.");
            throw new ApplicationException("Error refreshing tokens.", ex);
        }
    }
    public async System.Threading.Tasks.Task SyncLedgerToQuickBooksAsync(LedgerAccount ledgerAccount)
    {
        try
        {
            var storedTokens = await _oauthService.GetStoredTokensAsync();

            if (storedTokens == null || string.IsNullOrEmpty(storedTokens.AccessToken))
            {
                throw new InvalidOperationException("Access token is missing or invalid.");
            }

            var oauthValidator = new OAuth2RequestValidator(storedTokens.AccessToken);
            var serviceContext = new ServiceContext("9341453104198392", IntuitServicesType.QBO, oauthValidator);
            var dataService = new DataService(serviceContext);

            var account = new QueryService<LedgerAccount>(serviceContext)
                .ExecuteIdsQuery("Select * From Account Where AccountType='Accounts Receivable' StartPosition 1 MaxResults 1")
                .FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Accounts Receivable account not found in QuickBooks.");
            }

            var journalEntry = new JournalEntry
            {
                Line = new List<Line>
                {
                    new Line
                    {
                        Amount = (decimal)ledgerAccount.Balance,
                        DetailType = LineDetailTypeEnum.JournalEntryLineDetail,
                        AnyIntuitObject = new JournalEntryLineDetail
                        {
                            PostingType = PostingTypeEnum.Credit,
                            AccountRef = new ReferenceType { Value = account.Id.ToString() }
                        }
                    }
                }.ToArray(),
                TxnDate = DateTime.UtcNow
            };

            dataService.Add(journalEntry);
            _logger.LogInformation("Ledger account synced successfully with QuickBooks.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error syncing ledger account to QuickBooks.");
            throw new ApplicationException("Error syncing ledger account to QuickBooks.", ex);
        }
    }

    internal void SyncDonationToQuickBooks(Donation donation)
    {
        throw new NotImplementedException();
    }
}

// Ensure that MissingOAuthTokensException is defined only once
public class MissingOAuthTokensException : Exception
{
    public MissingOAuthTokensException(string message) : base(message)
    {
    }
}
