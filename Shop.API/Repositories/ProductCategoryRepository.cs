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

        public async Task<ProductCategory> AddProductCategory(ProductCategory productCategory)
        {
            _shopDbContext.ProductCategories.Add(productCategory);
            await _shopDbContext.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory)
        {
            _shopDbContext.Entry(productCategory).State = EntityState.Modified;
            await _shopDbContext.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> DeleteProductCategory(int id)
        {
            var productCategory = await _shopDbContext.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return false;
            }

            _shopDbContext.ProductCategories.Remove(productCategory);
            await _shopDbContext.SaveChangesAsync();
            return true;
        }
    }
}
