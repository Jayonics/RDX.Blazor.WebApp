using Shop.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.API.Repositories.Contracts
{
    /// <summary>
    /// Interface for ServiceRepository
    /// </summary>
    public interface IServiceRepository
    {
        /// <summary>
        /// Retrieves all services from the database.
        /// </summary>
        /// <returns>A collection of all services.</returns>
        Task<IEnumerable<Service>> GetServices();

        /// <summary>
        /// Retrieves a single service by its ID.
        /// </summary>
        /// <param name="id">The ID of the service.</param>
        /// <returns>The service with the provided ID.</returns>
        Task<Service> GetService(int id);

        /// <summary>
        /// Updates a service in the database.
        /// </summary>
        /// <param name="service">The updated service.</param>
        /// <returns>The updated service.</returns>
        Task<Service> UpdateService(Service service);

        /// <summary>
        /// Deletes a service from the database.
        /// </summary>
        /// <param name="id">The ID of the service to delete.</param>
        /// <returns>True if the service was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteService(int id);

        /// <summary>
        /// Adds a new service to the database.
        /// </summary>
        /// <param name="service">The service to add.</param>
        /// <returns>The added service.</returns>
        Task<Service> AddService(Service service);
    }
}
