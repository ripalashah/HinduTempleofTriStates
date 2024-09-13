using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HinduTempleofTriStates.Models;

namespace HinduTempleofTriStates.Services
{
    public interface IDonationService
    {
        Task AddCashTransactionForDonationAsync(Donation donation, bool isAddition);
        Task<bool> AddDonationAsync(Donation donation, bool isAddition); // Update to match the new signature
        Task<bool> DeleteDonationAsync(Guid id);
        Task<List<Donation>> GetDonationsAsync();
        Task<Donation?> GetDonationByIdAsync(Guid id);
        Task<bool> UpdateDonationAsync(Donation donation);
        Task UpdateLedgerAccountBalanceAsync(Guid? ledgerAccountId, double amount, bool isAddition = true);
        Task AddGeneralLedgerEntryForDonationAsync(Donation donation, bool isAddition = true);
    }
}
