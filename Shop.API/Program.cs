using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Repositories;
using Shop.API.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// If the computername is "INF-LAP-MSI1", use the DevDatabaseConnection connection string,
// if the computername is "JH-WIN-PC1", use the TestDatabaseConnection connection string,

var computerName = Environment.MachineName;
var connectionString = computerName switch
{
    "INF-LAP-MSI1" => builder.Configuration.GetConnectionString("DevDatabaseConnection"),
    "JH-WIN-PC1" => builder.Configuration.GetConnectionString("TestDatabaseConnection"),
    _ => builder.Configuration.GetConnectionString("DefaultConnection")
};

builder.Services.AddDbContextPool<ShopDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
