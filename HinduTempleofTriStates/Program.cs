using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Register ApplicationDbContext with dependency injection container
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .LogTo(Console.WriteLine, LogLevel.Information)); // Logging SQL queries for debugging

        // Register repositories with dependency injection container
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ILedgerRepository, LedgerRepository>(); // Ensure you have ILedgerRepository and LedgerRepository
        builder.Services.AddScoped<IDonationRepository, DonationRepository>(); // Register DonationRepository

        // Register services with dependency injection container
        builder.Services.AddScoped<LedgerService>(); // Register LedgerService
        builder.Services.AddScoped<DonationService>(); // Register DonationService

        // Build the application
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        // Uncomment the following line if you want to enforce HTTPS redirection
        // app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Configure endpoint routing
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Map additional routes if needed for the Accounts module
        // Example:
        // app.MapControllerRoute(
        //     name: "accounts",
        //     pattern: "Accounts/{action=Index}/{id?}",
        //     defaults: new { controller = "Accounts" });

        app.Run();
    }
}
