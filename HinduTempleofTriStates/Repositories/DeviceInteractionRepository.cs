using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Repositories
{
    public class DeviceInteractionRepository : IDeviceInteractionRepository
    {
        private readonly ApplicationDbContext _context;


        public DeviceInteractionRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Donation?> GetDonationByReceiptNumberAsync(string receiptNumber)
        {
            // Ensure that the receipt number is valid
            if (string.IsNullOrWhiteSpace(receiptNumber))
            {
                throw new ArgumentException("Receipt number cannot be null or empty.", nameof(receiptNumber));
            }

            // Retrieve the donation by receipt number
            return await _context.Donations.FirstOrDefaultAsync(d => d.ReceiptNumber == receiptNumber);
        }

        // Get all device interactions
        public async Task<List<DeviceInteraction>> GetAllInteractionsAsync()
        {
            return await _context.DeviceInteractions.ToListAsync();
        }

        // Get a specific interaction by ID
        public async Task<DeviceInteraction?> GetInteractionByIdAsync(Guid id)
        {
            return await _context.DeviceInteractions.FindAsync(id);
        }

        // Add a new device interaction
        public async Task AddInteractionAsync(DeviceInteraction interaction)
        {
            await _context.DeviceInteractions.AddAsync(interaction);
            await _context.SaveChangesAsync();
        }

        // Update an existing interaction
        public async Task UpdateInteractionAsync(DeviceInteraction interaction)
        {
            _context.DeviceInteractions.Update(interaction);
            await _context.SaveChangesAsync();
        }

        // Delete an interaction
        public async Task DeleteInteractionAsync(Guid id)
        {
            var interaction = await GetInteractionByIdAsync(id);
            if (interaction != null)
            {
                _context.DeviceInteractions.Remove(interaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
