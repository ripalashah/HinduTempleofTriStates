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
using HinduTempleofTriStates.Hubs;

internal class Program
{
    private static async Task Main(string[] args)
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
        builder.Services.AddScoped<IDonationService, DonationService>();
        builder.Services.AddScoped<FundService>();
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ILedgerRepository, LedgerRepository>();
        builder.Services.AddScoped<IDonationRepository, DonationRepository>();
        builder.Services.AddScoped<IFundRepository, FundRepository>();
        builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
        builder.Services.AddScoped<IFinancialReportService, FinancialReportService>();
        builder.Services.AddScoped<IInventoryService, InventoryService>();
        builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
        builder.Services.AddScoped<ISupplierService, SupplierService>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddTransient<EmailService>();
        builder.Services.AddScoped<QuickBooksService>(); // Add QuickBooksService to DI
        builder.Services.AddScoped<OAuthService>();
        builder.Services.Configure<QuickBooksSettings>(builder.Configuration.GetSection("QuickBooks"));
        builder.Services.AddSession(); // Add this line in the service configuration section
        builder.Services.AddSignalR();
        builder.Services.AddScoped<IDeviceIntegrationService, DeviceIntegrationService>();
        builder.Services.AddScoped<IDeviceInteractionRepository, DeviceInteractionRepository>(); // Register the repository

        // Role Management
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<RoleRepository>();
        builder.Services.AddScoped<RoleManager<IdentityRole>>(); // RoleManager for role management

        // Get the connection string from appsettings.json and register ApplicationDbContext
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .LogTo(Console.WriteLine, LogLevel.Information)); // Log SQL queries for debugging

        // Register DonationService after ApplicationDbContext
        builder.Services.AddScoped<IDonationService, DonationService>();
        // Register QuickBooksSettingsService as a scoped service
        builder.Services.AddScoped<QuickBooksSettingsService>();
        // Add Identity for user authentication and role management
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Configure password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddDefaultUI();

        // Configure application cookies for login and access control
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Redirect to the login page when not authenticated
            options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to access denied page when unauthorized
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie expiration time
            options.SlidingExpiration = true; // Sliding expiration for cookie renewal
            options.Cookie.HttpOnly = true; // HTTP-only cookie for security
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Always use secure cookies
            options.Cookie.SameSite = SameSiteMode.Strict; // Strict SameSite policy
            options.Cookie.Name = "YourAppAuthCookie"; // Customize the cookie name if needed
        });

        // Authorization policies to restrict access based on roles
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireAccountantRole", policy => policy.RequireRole("Accountant"));
            options.AddPolicy("RequireCounterRole", policy => policy.RequireRole("Counter"));
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });
        // Add session services
        builder.Services.AddDistributedMemoryCache(); // Required for session state
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
            options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
            options.Cookie.IsEssential = true; // Make the session cookie essential
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
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = context =>
            {
                if (!app.Environment.IsDevelopment())
                {
                    context.Context.Response.Headers["Cache-Control"] = "public,max-age=604800"; // 7 days caching for static files
                }
            }
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        // Use session middleware
        app.UseSession();
        // Configure cookie policy for security
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.Strict
        });

        // Add basic security headers for additional security
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("X-Frame-Options", "DENY");
            await next();
        });
        app.UsePathBase("/htts");

        app.MapHub<BookingHub>("/bookingHub"); // Map the BookingHub to this route
        app.MapHub<DonationHub>("/donationHub");  // Map the DonationHub to its route

        // Default MVC route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // This is optional, but helpful to define the Admin area specifically.
        app.MapControllerRoute(
            name: "admin",
            pattern: "{controller=Admin}/{action=ManageUsers}/{id?}");
        
        // Map Razor Pages routes
        app.MapRazorPages();

        // Seed roles
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.Initialize(roleManager, userManager); // Ensure roles and admin user are seeded
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        // Run the application
        app.Run();
    }
}

// Class to initialize roles and the default admin user
public static class RoleInitializer
{
    public static async Task Initialize(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        string[] roleNames = { "Admin", "Accountant", "Counter" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Seed default admin user
        var adminEmail = "admin@temple.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@12345"); // Set a secure password

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
