using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllersWithViews();

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        // Add services to the container for MVC controllers, views, and Razor Pages
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages(); // Adds support for Razor Pages
        builder.Logging.AddFile("Logs/app-log.txt");
        // Dependency injection for your services and repositories
        builder.Services.AddScoped<ICashTransactionService, CashTransactionService>(); // CashTransaction Service
        builder.Services.AddScoped<IAccountService, AccountService>(); // Account Service
        builder.Services.AddScoped<LedgerService>(); // Ledger Service
        builder.Services.AddScoped<DonationService>(); // Donation Service
        builder.Services.AddScoped<FundService>(); // Fund Service
        builder.Services.AddScoped<IReportService, ReportService>(); // Report Service
        builder.Services.AddScoped<IDonationService, DonationService>();
        builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
        // Register repositories with DI
        builder.Services.AddScoped<IAccountRepository, AccountRepository>(); // Account repository
        builder.Services.AddScoped<ILedgerRepository, LedgerRepository>(); // Ledger repository
        builder.Services.AddScoped<IDonationRepository, DonationRepository>(); // Donation repository
        builder.Services.AddScoped<IFundRepository, FundRepository>(); // Fund repository

        // Get the connection string from appsettings.json and register the ApplicationDbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection String: {connectionString}");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .LogTo(Console.WriteLine, LogLevel.Information)); // Logging SQL queries for debugging

        // Add Identity (use AddIdentity instead of AddDefaultIdentity for more control over Identity)
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Build the application
        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // Use HSTS in production
        }
        else
        {
            app.UseDeveloperExceptionPage(); // Detailed error page for development
        }

        // Uncomment the following line if you want to enforce HTTPS redirection
        // app.UseHttpsRedirection();

        app.UseStaticFiles(); // Serve static files from the wwwroot folder
        app.UseRouting(); // Enable routing
        app.UseAuthentication(); // Enable authentication
        app.UseAuthorization(); // Enable authorization
        app.UseHttpsRedirection();
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.Strict
        });
        // Add route logging middleware
        app.Use(async (context, next) =>
        {
            // Log the current route that is being accessed
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
        // Map default MVC controller route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Map Razor Pages routes
        app.MapRazorPages();

        // Run the application
        app.Run();
    }
}
