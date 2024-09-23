using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Models;
using Microsoft.EntityFrameworkCore; // Ensure this points to the correct class

public class QuickBooksSettingsService
{
    private readonly ApplicationDbContext _dbContext;

    public QuickBooksSettingsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Use the correct return type based on your project’s need
    public async Task<QuickBooksSettings> GetQuickBooksSettingsAsync()
    {
        // Ensure you are querying the correct DbSet and type
        var settings = await _dbContext.QuickBooksSettings.FirstOrDefaultAsync();
        if (settings == null)
        {
            throw new InvalidOperationException("QuickBooks settings are not configured in the database.");
        }

        return settings; // Return the correct type
    }
}
