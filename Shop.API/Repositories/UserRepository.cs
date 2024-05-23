using Microsoft.EntityFrameworkCore;
using Shop.Shared.Data;
using Shop.Shared.Entities;
using Shop.API.Repositories.Contracts;

namespace Shop.API.Repositories
{
    public class UserRepository(UserDbContext dbContext) : IUserRepository
    {

        public Task<ApplicationUser> GetUser(int id) => throw new NotImplementedException();

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            var users = await dbContext.Users.ToListAsync();

            return users;
        }
    }
}
