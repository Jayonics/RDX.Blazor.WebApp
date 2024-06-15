using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Models.Requests;
using Shop.Models.Responses;
using Shop.API.Repositories.Contracts;
using Shop.Shared.Data;
using Shop.Shared.Entities;

namespace Shop.API.Repositories
{
    public class MediaRepository : IMediaRepository
    {

        #region Dependency Injection / Constructor

        private readonly ILogger<MediaRepository> _logger;

        private readonly ShopDbContext _shopDbContext;

        private readonly IConfiguration _configuration;

        public MediaRepository(ShopDbContext shopDbContext, ILogger<MediaRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _shopDbContext = shopDbContext;
        }

        #endregion

        public async Task<IEnumerable<ProductImage>> GetAllProductImagesAsync()
        {
            return await _shopDbContext.ProductImages
                   .Include(pi => pi.Product)
                   .ToListAsync()
                   ?? throw new ArgumentException("No product images found");
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await _shopDbContext.ProductImages
                   .Where(pi => pi.ProductId == productId)
                   .ToListAsync()
                   ?? throw new ArgumentException($"No product images found for product with ID {productId}");
        }

        public async Task<ProductImage> GetProductImageAsync(int id)
        {
            return await _shopDbContext.ProductImages
                   .FirstOrDefaultAsync(pi => pi.Id == id)
                   ?? throw new ArgumentException($"Product image with ID {id} not found");
        }

        public async Task<ProductImage> UpdateProductImageAsync(ProductImage productImage)
        {
            var existingProductImage = await _shopDbContext.ProductImages.FindAsync(productImage.Id);
            if (existingProductImage == null)
            {
                throw new ArgumentException($"Product image with ID {productImage} not found");
            }

            try
            {
                _shopDbContext.Entry(existingProductImage).CurrentValues.SetValues(productImage);
                await _shopDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception
                throw new DbUpdateException($"An error occurred while updating product image with ID {productImage.Id}", ex);
            }

            return productImage;
        }

        public async Task<ProductImage> AddProductImageAsync(ProductImage productImage)
        {
            _logger.LogDebug($"Attempting to add product {productImage.Name} to {productImage.ProductId}");
            try
            {
                // If the productImage.Id is not 0, throw an exception
                if (productImage.Id is not 0)
                {
                    _logger.LogError($"Product Image ID must be 0. ({productImage.Name})");
                    throw new ArgumentException("Product Image ID must be 0.");
                }
                else if (productImage.ProductId is 0)
                {
                    _logger.LogError($"Product ID must reference a valid product. ({productImage.Name})");
                    throw new ArgumentException("Product ID must reference a valid product.");
                }
                // Add the product image to the database
                await _shopDbContext.ProductImages.AddAsync(productImage);
                await _shopDbContext.SaveChangesAsync();

                return productImage;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception and return null or throw an exception
                _logger.LogError($"An error occurred while adding product image {productImage.Name}.", ex);
                throw new Exception("An error occurred while adding product image.", ex);
            }
        }

        public async Task<bool> DeleteProductImageAsync(int id)
        {
            var productImage = await _shopDbContext.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                throw new ArgumentException("Product image not found with the provided id.", nameof(id));
            }

            try
            {
                _shopDbContext.ProductImages.Remove(productImage);
                await _shopDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while deleting product image {productImage.Name}.", ex);
                throw;
            }

            return true;
        }

        public async Task<bool> DeleteProductImagesAsync(int productId)
        {
            var productImages = await _shopDbContext.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();
            if (!productImages.Any()) return false;

            try
            {
                _shopDbContext.ProductImages.RemoveRange(productImages);
                await _shopDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while deleting product images for product with ID {productId}.", ex);
                throw new Exception("An error occurred while deleting product images.", ex);
            }

            return true;
        }
    }
}
