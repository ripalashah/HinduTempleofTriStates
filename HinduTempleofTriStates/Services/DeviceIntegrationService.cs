using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class DeviceIntegrationService : IDeviceIntegrationService
    {
        private readonly IDeviceInteractionRepository _interactionRepository;
        private readonly IDonationService _donationService;
        private readonly ILogger<DeviceIntegrationService> _logger;

        public DeviceIntegrationService(
            IDeviceInteractionRepository interactionRepository,
            IDonationService donationService,
            ILogger<DeviceIntegrationService> logger)
        {
            _interactionRepository = interactionRepository ?? throw new ArgumentNullException(nameof(interactionRepository));
            _donationService = donationService ?? throw new ArgumentNullException(nameof(donationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Method to handle device interactions, specifically donations
        public async Task HandleDonationFromDeviceAsync(Donation donation, string deviceName)
        {
            try
            {
                // Ensure donation is unique by checking if it already exists
                if (await IsDonationDuplicateAsync(donation))
                {
                    _logger.LogWarning("Duplicate donation detected from device {DeviceName} with Receipt Number {ReceiptNumber}.", deviceName, donation.ReceiptNumber);
                    return;
                }

                // Add donation using DonationService
                var result = await _donationService.AddDonationAsync(donation, true);

                if (!result)
                {
                    _logger.LogError("Failed to add donation from device {DeviceName}.", deviceName);
                    throw new InvalidOperationException("Failed to process donation.");
                }

                // Log and create a new DeviceInteraction record
                var interaction = new DeviceInteraction
                {
                    Id = Guid.NewGuid(),
                    DonationId = donation.Id,
                    DeviceName = deviceName,
                    InteractionDate = DateTime.UtcNow,
                    Action = "CreateDonation",
                    Status = "Completed",
                    ResponseMessage = "Donation successfully processed."
                };

                await _interactionRepository.AddInteractionAsync(interaction);

                _logger.LogInformation("Donation processed from device {DeviceName} successfully.", deviceName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing donation from device {DeviceName}.", deviceName);

                // Log failed interaction
                var interaction = new DeviceInteraction
                {
                    Id = Guid.NewGuid(),
                    DonationId = donation.Id,
                    DeviceName = deviceName,
                    InteractionDate = DateTime.UtcNow,
                    Action = "CreateDonation",
                    Status = "Failed",
                    ResponseMessage = ex.Message
                };

                await _interactionRepository.AddInteractionAsync(interaction);
            }
        }
        // Implement the missing method, ensuring it does not return null
        public async Task<Donation> GetDonationByReceiptNumberAsync(string receiptNumber)
        {
            // Retrieve the donation from the repository
            var existingDonation = await _interactionRepository.GetDonationByReceiptNumberAsync(receiptNumber);

            // Check if the donation is null and handle the case appropriately
            if (existingDonation == null)
            {
                // You can throw an exception or handle this scenario as per your requirement
                throw new InvalidOperationException($"Donation with receipt number '{receiptNumber}' not found.");
            }

            return existingDonation;
        }

        // Check for duplicate donations
        // Your existing methods
        private async Task<bool> IsDonationDuplicateAsync(Donation donation)
        {
            // Ensure that the receipt number is valid
            if (string.IsNullOrWhiteSpace(donation.ReceiptNumber))
            {
                throw new ArgumentException("Receipt number cannot be null or empty.", nameof(donation.ReceiptNumber));
            }

            // Call the repository method and handle potential null
            var existingDonation = await _interactionRepository.GetDonationByReceiptNumberAsync(donation.ReceiptNumber);
            // Return true if a donation with the same receipt number exists
            return existingDonation is not null;
        }

        // Retrieve all interactions
        public async Task<List<DeviceInteraction>> GetAllInteractionsAsync()
        {
            return await _interactionRepository.GetAllInteractionsAsync();
        }
    }
}
