using HinduTempleofTriStates.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public interface IDeviceInteractionRepository
    {
        Task<List<DeviceInteraction>> GetAllInteractionsAsync();
        Task<DeviceInteraction?> GetInteractionByIdAsync(Guid id);
        Task AddInteractionAsync(DeviceInteraction interaction);
        Task UpdateInteractionAsync(DeviceInteraction interaction);
        Task DeleteInteractionAsync(Guid id);
        Task<Donation?> GetDonationByReceiptNumberAsync(string receiptNumber);
    }
}
