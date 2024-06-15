using Microsoft.EntityFrameworkCore;
using Shop.Shared.Data;
using Shop.Shared.Entities;
using Shop.API.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Shop.Shared.Extensions;

namespace Shop.API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        #region Dependency Injection / Constructor
        private readonly ILogger<ProductRepository> _logger;
        private readonly ShopDbContext _shopDbContext;
        private readonly IConfiguration _configuration;

        public ProductRepository(ShopDbContext shopDbContext, ILogger<ProductRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _shopDbContext = shopDbContext;
        }
        #endregion

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await _shopDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await _shopDbContext.ProductCategories.FindAsync(id);
            return category;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _shopDbContext.Products
                .Include(p => p.Images)// Include the Images when retrieving the Product
                .Include(p => p.Category)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Images = p.Images,
                    // If Images is not null, set the ImageURL to the first image in the collection, otherwise use the placeholder image
                    ImageURL = p.Images.Count != 0 ? p.Images.FirstOrDefault().Name.FormatImageUrl(_configuration["Storage:BlobContainerURL"]) : "https://via.placeholder.com/150"
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _shopDbContext.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Images = p.Images,
                    ImageURL = p.Images.Any() ? p.Images.FirstOrDefault().Name.FormatImageUrl(_configuration["Storage:BlobContainerURL"]) : "https://via.placeholder.com/100"
                })
                .ToListAsync();

            return products;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existingProduct = await _shopDbContext.Products.FindAsync(product.Id);
            if (existingProduct == null) throw new ArgumentException($"Product with ID {product.Id} not found.");

            try
            {
                _shopDbContext.Entry(existingProduct).CurrentValues.SetValues(product);
            }
            catch (ArgumentException argEx)
            {
                // Handle the exception
                throw new Exception("An error occurred while updating the product.", argEx);
            }

            try
            {
                await _shopDbContext.SaveChangesAsync();
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
            var product = await _shopDbContext.Products.FindAsync(id);
            if (product == null) return false;

            try
            {
                _shopDbContext.Products.Remove(product);
                await _shopDbContext.SaveChangesAsync();
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
            _logger.LogDebug($"Attempting to add product {product.Name} to the database.");
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

                await _shopDbContext.Products.AddAsync(product);
                await _shopDbContext.SaveChangesAsync();
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
