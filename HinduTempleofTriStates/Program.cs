using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HinduTempleofTriStates.Data;
using HinduTempleofTriStates.Repositories;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Identity.UI;

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
        builder.Logging.AddFile("Logs/app-log.txt"); // Log to file

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

        // Role Management
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<RoleRepository>();
        // Ensure RoleManager is added
        builder.Services.AddScoped<RoleManager<IdentityRole>>();

        // Get the connection string from appsettings.json and register ApplicationDbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .LogTo(Console.WriteLine, LogLevel.Information)); // Log SQL queries for debugging

        // Add Identity for user authentication and role management
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()  // Enables default UI for Identity
            .AddDefaultTokenProviders();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireAccountantRole", policy => policy.RequireRole("Accountant"));
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Redirects to the login page when not authenticated
            options.AccessDeniedPath = "/Account/AccessDenied"; // Redirects to access denied page
        });

        // Build the application
        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts(); // Use HSTS in production environments
        }
        else
        {
            app.UseDeveloperExceptionPage(); // Detailed error page for development
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        // Configure cookie policy for security
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.Strict
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
