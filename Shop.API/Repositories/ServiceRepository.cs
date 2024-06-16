using Microsoft.EntityFrameworkCore;
using Shop.Shared.Data;
using Shop.Shared.Entities;
using Shop.API.Repositories.Contracts;

namespace Shop.API.Repositories
{
    /// <summary>
    /// Repository for managing services in the database.
    /// </summary>
    public class ServiceRepository : IServiceRepository
    {
        private readonly ShopDbContext _shopDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepository"/> class.
        /// </summary>
        /// <param name="shopDbContext">The database context.</param>
        public ServiceRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        /// <summary>
        /// Retrieves all services from the database.
        /// </summary>
        /// <returns>A collection of all services.</returns>
        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _shopDbContext.Services.ToListAsync();
        }

        /// <summary>
        /// Retrieves a single service by its ID.
        /// </summary>
        /// <param name="id">The ID of the service.</param>
        /// <returns>The service with the provided ID.</returns>
        public async Task<Service> GetService(int id)
        {
            var service = await _shopDbContext.Services.FindAsync(id);
            if (service == null) throw new ArgumentException($"Service with ID {id} not found.");

            return service;
        }

        /// <summary>
        /// Updates a service in the database.
        /// </summary>
        /// <param name="service">The updated service.</param>
        /// <returns>The updated service.</returns>
        public async Task<Service> UpdateService(Service service)
        {
            var existingService = await _shopDbContext.Services.FindAsync(service.Id);
            if (existingService == null) throw new ArgumentException($"Service with ID {service.Id} not found.");

            _shopDbContext.Entry(existingService).CurrentValues.SetValues(service);
            await _shopDbContext.SaveChangesAsync();

            return existingService;
        }

        /// <summary>
        /// Deletes a service from the database.
        /// </summary>
        /// <param name="id">The ID of the service to delete.</param>
        /// <returns>True if the service was deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteService(int id)
        {
            var service = await _shopDbContext.Services.FindAsync(id);
            if (service == null) throw new ArgumentException($"Service with ID {id} not found.");

            _shopDbContext.Services.Remove(service);
            await _shopDbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Adds a new service to the database.
        /// </summary>
        /// <param name="service">The service to add.</param>
        /// <returns>The added service.</returns>
        public async Task<Service> AddService(Service service)
        {
            if (service.Id != 0)
            {
                throw new ArgumentException("The service ID must be 0 or null to ensure a new ID is generated.");
            }

            await _shopDbContext.Services.AddAsync(service);
            await _shopDbContext.SaveChangesAsync();

            return service;
        }
    }
}