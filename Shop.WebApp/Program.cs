using System.Diagnostics;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.WebApp.Components;
using Shop.WebApp.Components.Account;
using Shop.Shared.Data;
using Shop.Shared.Entities;
using Shop.WebApp.Services;
using Shop.WebApp.Services.Contracts;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Templates;

namespace Shop.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Get the machine name
                var computerName = Environment.MachineName;

                builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.{computerName}.json", optional: true)
                .AddEnvironmentVariables();

                // Add the Serilog Service to the container (NuGet package).
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateBootstrapLogger();

                builder.Services.AddSerilog((services, lc) => lc
                    .ReadFrom.Configuration(builder.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    // Default File Log
                    .WriteTo.Logger(x =>
                    {
                        x.Filter.ByExcluding(Matching.WithProperty<string>("SourceContext", sc => sc == "Microsoft.EntityFrameworkCore.Database.Command"));
                        x.WriteTo.File(
                        path: "Logs/Logs_.log",
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level} <{SourceContext}>] {Message:lj}{NewLine}{Exception}",
                        rollingInterval: RollingInterval.Day
                        );
                    })
                    .WriteTo.Logger(x =>
                    {
                        // Filter "Microsoft.EntityFrameworkCore.Database.Command" SourceContext logs from the default loggers and log exclusively to the SqlLogs.txt file.
                        x.Filter.ByIncludingOnly(Matching.WithProperty<string>("SourceContext", sc => sc == "Microsoft.EntityFrameworkCore.Database.Command"));
                        x.WriteTo.File(
                            path: "Logs/EntityFrameworkLogs_.log",
                            restrictedToMinimumLevel: LogEventLevel.Information,
                            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level} <{SourceContext}>] {Message:lj}{NewLine}{Exception}",
                            rollingInterval: RollingInterval.Day
                        );
                    })
                    .WriteTo.Logger(x =>
                    {
                        x.Filter.ByExcluding(Matching.WithProperty<string>("SourceContext", sc => sc == "Microsoft.EntityFrameworkCore.Database.Command"));
                        // Exclude "Microsoft.EntityFrameworkCore.Database.Command" SourceContext logs from the console logger.
                        x.WriteTo.Console(
                            theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code,
                            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level}] [HTTP:<ReqID:{RequestId}>] [<s:{SourceContext}>|<c:{ClassName}>|<m:{MethodName}>] {Message:lj}{NewLine}{Exception}"
                        );
                        // x.WriteTo.Console(
                        // new ExpressionTemplate("[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level}] [<s:{SourceContext}>{#if ClassName is not null}|<c:{ClassName}>{#end}{if MethodName is not null}|<m:{MethodName}>{#end}] {Message:lj}{NewLine}{Exception}")
                        // );
                    })
                );

                // Add the BlazorBoostrap Service to the container (NuGet package).
                builder.Services.AddBlazorBootstrap();

                // Add services to the container.
                builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
                // .AddInteractiveWebAssemblyComponents();

                builder.Services.AddCascadingAuthenticationState();
                builder.Services.AddScoped<IdentityUserAccessor>();
                builder.Services.AddScoped<IdentityRedirectManager>();
                builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

                // Select the connection string based on the machine name
                var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

                builder.Services.AddDbContextPool<UserDbContext>(options =>
                options.UseSqlServer(connectionString));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<UserDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

                builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7015/") });
                builder.Services.AddScoped<IProductService, ProductService>();
                builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
                builder.Services.AddScoped<IStorageService, StorageService>();

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

                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                    app.UseDeveloperExceptionPage();
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
                // .AddInteractiveWebAssemblyRenderMode();

                // Add additional endpoints required by the Identity /Account Razor components.
                app.MapAdditionalIdentityEndpoints();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
