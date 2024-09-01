using Microsoft.EntityFrameworkCore;
using TempleManagementSystem.Models;

namespace TempleManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Donation> Donations { get; set; }
    }
}