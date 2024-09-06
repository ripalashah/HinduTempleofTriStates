using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container for MVC controllers, views, and Razor Pages
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages(); // Adds support for Razor Pages
        builder.Services.AddScoped<ICashTransactionService, CashTransactionService>();
        // Get the connection string from appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection String: {connectionString}");
        builder.Services.AddScoped<IAccountService, AccountService>();
        // Register ApplicationDbContext with dependency injection container
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .LogTo(Console.WriteLine, LogLevel.Information)); // Logging SQL queries for debugging

        // Register repositories with dependency injection container
        builder.Services.AddScoped<IAccountRepository, AccountRepository>(); // Account repository
        builder.Services.AddScoped<ILedgerRepository, LedgerRepository>(); // Ledger repository
        builder.Services.AddScoped<IDonationRepository, DonationRepository>(); // Donation repository
        builder.Services.AddScoped<IFundRepository, FundRepository>(); // Fund repository (new)

        // Register services with dependency injection container
        builder.Services.AddScoped<LedgerService>(); // LedgerService
        builder.Services.AddScoped<DonationService>(); // DonationService
        builder.Services.AddScoped<FundService>(); // FundService (new)
        builder.Services.AddScoped<IReportService, ReportService>();


        // Add Identity (use AddIdentity instead of AddDefaultIdentity)
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Build the application
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // Use HSTS in production
        }
        else
        {
            app.UseDeveloperExceptionPage(); // Developer exception page for detailed errors in development
        }

        // Uncomment the following line if you want to enforce HTTPS redirection
        // app.UseHttpsRedirection();

        app.UseStaticFiles(); // Serve static files from the wwwroot folder
        app.UseRouting(); // Enable routing
        app.UseAuthentication(); // Enable authentication
        app.UseAuthorization(); // Enable authorization

        // Map Razor Pages routes
        app.MapRazorPages();

        // Map default MVC controller route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Run the application
        app.Run();
    }
}
