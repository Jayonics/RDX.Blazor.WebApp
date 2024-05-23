using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Shop.Shared.Data;
using Shop.API.Repositories;
using Shop.API.Repositories.Contracts;

// Entry point of the application
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controllers to the services
builder.Services.AddControllers();
// Add API explorer endpoints to the services
builder.Services.AddEndpointsApiExplorer();
// Add Swagger generator to the services
builder.Services.AddSwaggerGen();
// Add Authorization to the services
builder.Services.AddAuthorization();

// If the computername is "INF-LAP-MSI1", use the DevDatabaseConnection connection string,
// if the computername is "JH-WIN-PC1", use the TestDatabaseConnection connection string,

// Get the machine name
var computerName = Environment.MachineName;
// Select the connection string based on the machine name
var connectionString = computerName switch
{
    "INF-LAP-MSI1" => builder.Configuration.GetConnectionString("DevDatabaseConnection"),
    "JH-WIN-PC1" => builder.Configuration.GetConnectionString("TestDatabaseConnection"),
    _ => builder.Configuration.GetConnectionString("DefaultConnection")
};

// This is the Dependency Injection (DI) container

// Add a database context to the services with a connection pool
builder.Services.AddDbContextPool<ShopDbContext>(options => {
    // Use SQL Server with the selected connection string
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContextPool<UserDbContext>(options => {
    // Use SQL Server with the selected connection string
    options.UseSqlServer(connectionString);
});

// Add the product repository to the services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Add the user repository to the services
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add the product category repository to the services
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
// If the environment is development
if (app.Environment.IsDevelopment())
{
    // Use Swagger
    app.UseSwagger();
    // Use Swagger UI
    app.UseSwaggerUI();
}

// Allow Web project CORS
app.UseCors(policy =>
policy.WithOrigins("https://localhost:7138", "http://localhost:7138")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
);

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use authorization
app.UseAuthorization();

// Map the controllers
app.MapControllers();

/*// Map Identity Routes
app.MapIdentityApi<ApplicationUser>();*/

// Run the application
app.Run();
