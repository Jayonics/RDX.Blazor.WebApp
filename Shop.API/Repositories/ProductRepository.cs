using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Entities;
using Shop.API.Repositories.Contracts;

namespace Shop.API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        readonly ILogger _logger;

        readonly ShopDbContext shopDbContext;

        public ProductRepository(ShopDbContext shopDbContext, ILogger<ProductRepository> logger)
        {
            _logger = logger;
            this.shopDbContext = shopDbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await shopDbContext.ProductCategories.ToListAsync();
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
            var products = await shopDbContext.Products.ToListAsync();

            return products;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existingProduct = await shopDbContext.Products.FindAsync(product.Id);
            if (existingProduct == null) throw new ArgumentException($"Product with ID {product.Id} not found.");

            try
            {
                shopDbContext.Entry(existingProduct).CurrentValues.SetValues(product);
            }
            catch (ArgumentException argEx)
            {
                // Handle the exception (log it, rethrow it, etc.)
                throw new Exception("An error occurred while updating the product.", argEx);
            }

            try
            {
                await shopDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception and return null or throw an exception
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }

            return existingProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await shopDbContext.Products.FindAsync(id);
            if (product == null) return false;

            try
            {
                shopDbContext.Products.Remove(product);
                await shopDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception and return false or throw an exception
                throw new Exception("An error occurred while deleting the product.", ex);
            }

            return true;
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                // Log a warning if the product ID is not 0 or null
                if (product.Id is not 0)
                // Log a warning
                    _logger.LogWarning(
                    "The product ID must be equivalent to 0 or null to ensure a new ID is generated.\n " +
                    "The product ID was not 0 or null.");

                // Strip the ID from the product to ensure a new ID is generated
                product.Id = 0;

                await shopDbContext.Products.AddAsync(product);
                await shopDbContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                // Log the exception and return null or throw an exception
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }
    }
}
