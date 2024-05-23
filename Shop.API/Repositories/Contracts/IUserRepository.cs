using Shop.Shared.Entities;

namespace Shop.API.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUser(int id);
    }
}
