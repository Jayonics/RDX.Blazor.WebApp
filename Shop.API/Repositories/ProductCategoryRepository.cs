using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Entities;
using Shop.API.Repositories.Contracts;

namespace Shop.API.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly ILogger _logger;

        public ProductCategoryRepository(ShopDbContext shopDbContext, ILogger<ProductCategoryRepository> logger)
        {
            _logger = logger;
            this._shopDbContext = shopDbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            var categories = await this._shopDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetProductCategory(int id)
        {
            var category = await _shopDbContext.ProductCategories.FindAsync(id);
            return category;
        }
    }
}
