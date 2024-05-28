using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.WebApp.Components;
using Shop.WebApp.Components.Account;
using Shop.Shared.Data;
using Shop.Shared.Entities;
using Shop.WebApp.Services;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add the BlazorBoostrap Service to the container (NuGet package).
            builder.Services.AddBlazorBootstrap();

            // Add services to the container.
            builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            // Get the machine name
            var computerName = Environment.MachineName;

            builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddJsonFile($"appsettings.{computerName}.json", optional: true)
            .AddEnvironmentVariables();

            // Select the connection string based on the machine name
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            builder.Services.AddDbContextPool<UserDbContext>(options =>
            options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<UserDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7015/") });
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

            builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            builder.Services.AddCascadingAuthenticationState();
            // Admin only
            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Admin", configurePolicy: policy => policy.RequireRole("Admin"));
            });
            // Staff only
            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Staff", configurePolicy: policy => policy.RequireRole("Staff"));
            });
            // Admin or Staff
            builder.Services.AddAuthorization(options => {
                options.AddPolicy("AdminOrStaff", configurePolicy: policy => policy.RequireRole("Admin", "Staff"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
