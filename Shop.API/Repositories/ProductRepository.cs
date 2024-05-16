using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Entities;
using Shop.API.Repositories.Contracts;

namespace Shop.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext shopDbContext;

        public ProductRepository(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.shopDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await shopDbContext.ProductCategories.FindAsync(id);
            return category;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await shopDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await this.shopDbContext.Products.ToListAsync();

            return products;
        }

        public async Task UpdateProduct(Product product)
        {
            shopDbContext.Entry(product).State = EntityState.Modified;
            await shopDbContext.SaveChangesAsync();
        }
    }
}
