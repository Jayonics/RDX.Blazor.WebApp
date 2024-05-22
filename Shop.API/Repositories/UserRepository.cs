using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Entities;
using Shop.API.Repositories.Contracts;

namespace Shop.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly ShopDbContext shopDbContext;

        public UserRepository(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }

        public Task<User> GetUser(int id) => throw new NotImplementedException();

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await shopDbContext.Users.ToListAsync();

            return users;
        }
    }
}
