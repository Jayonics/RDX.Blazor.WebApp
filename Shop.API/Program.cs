using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Shop.Shared.Data;
using Shop.API.Repositories;
using Shop.API.Repositories.Contracts;
using Microsoft.Extensions.Azure;

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

// Get the machine name
var computerName = Environment.MachineName;

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddJsonFile($"appsettings.{computerName}.json", optional: true)
    .AddEnvironmentVariables();

// Select the connection string based on the machine name
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

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

/* For the Azurite Storage Emulator */
// With connection string
//builder.Services.AddAzureClients(clientBuilder => {
//    clientBuilder.AddBlobServiceClient(builder.Configuration["StorageConnectionString:blob"]!, preferMsi: true);
//    clientBuilder.AddQueueServiceClient(builder.Configuration["StorageConnectionString:queue"]!, preferMsi: true);
//});
builder.Services.AddAzureClients(clientBuilder => {
    clientBuilder.AddBlobServiceClient(
        builder.Configuration["StorageConnectionString:blob"]!
        );
});


// Activate Identity APIs
/*builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
.AddEntityFrameworkStores<UserDbContext>();*/

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
