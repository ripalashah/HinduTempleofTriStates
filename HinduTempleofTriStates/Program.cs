using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container for MVC, Razor Pages, and logging
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.AddFile("Logs/app-log.txt"); // Log to file (ensure you have the required logging library for file logging)

        // Dependency injection for services and repositories
        builder.Services.AddScoped<ICashTransactionService, CashTransactionService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<LedgerService>();
        builder.Services.AddScoped<DonationService>();
        builder.Services.AddScoped<FundService>();
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<IDonationService, DonationService>();
        builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ILedgerRepository, LedgerRepository>();
        builder.Services.AddScoped<IDonationRepository, DonationRepository>();
        builder.Services.AddScoped<IFundRepository, FundRepository>();

        // Get the connection string from appsettings.json and register ApplicationDbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection String: {connectionString}");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .LogTo(Console.WriteLine, LogLevel.Information)); // Log SQL queries for debugging

        // Add Identity for user authentication and role management
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Build the application
        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // Use HSTS in production environments
        }
        else
        {
            app.UseDeveloperExceptionPage(); // Detailed error page for development
        }

        // Uncomment the following line if you want to enforce HTTPS redirection
        app.UseHttpsRedirection();

        // Serve static files (e.g., images, CSS, JS) from the wwwroot folder
        app.UseStaticFiles();

        // Enable routing and authentication/authorization
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        // Configure cookie policy for security
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.Strict
        });

        // Route logging middleware
        app.Use(async (context, next) =>
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                Console.WriteLine($"Route accessed: {endpoint.DisplayName}");
            }
            await next();
        });

        // Inspect routing table on request to /routes
        app.MapGet("/routes", async context =>
        {
            var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
            foreach (var endpoint in endpointDataSource.Endpoints)
            {
                await context.Response.WriteAsync(endpoint.DisplayName + "\n");
            }
        });

        // Default MVC route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Map Razor Pages routes
        app.MapRazorPages();

        // Run the application
        app.Run();
    }
}
