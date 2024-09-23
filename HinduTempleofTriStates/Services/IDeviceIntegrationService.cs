using HinduTempleofTriStates.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public interface IDeviceIntegrationService
    {
        Task HandleDonationFromDeviceAsync(Donation donation, string deviceName);
        Task<List<DeviceInteraction>> GetAllInteractionsAsync();
        Task<Donation> GetDonationByReceiptNumberAsync(string receiptNumber);
    }
}
