using Shop.Models.Dtos;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp.Services
{
    public class ProductService : IProductService
    {
        readonly HttpClient httpClient;

        readonly ILogger<ProductService> logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
            return products;
        }
        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await httpClient.GetFromJsonAsync<ProductDto>($"api/Product/{id}");
            if (product == null) throw new Exception("Product not found");
            return product;
        }

        public async Task<ProductDto> UpdateProduct(ProductDto product)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/Product/{product.Id}", product);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Product updated successfully {response.StatusCode}");
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                logger.LogWarning($"Product update failed with status code {response.StatusCode}");
                // You can throw an exception or return null based on your error handling strategy
                throw new Exception($"Product update failed with status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                // Log exception
                logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Product deleted successfully {response.StatusCode}");
                    return true;
                }
                logger.LogWarning($"Product delete failed with status code {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                // Log exception
                logger.LogError(ex, "Error deleting product");
                throw;
            }
        }

        public async Task<ProductDto> AddProduct(ProductDto product)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Product", product);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Product added successfully {response.StatusCode}");
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                logger.LogWarning($"Product add failed with status code {response.StatusCode}");
                // You can throw an exception or return null based on your error handling strategy
                throw new Exception($"Product add failed with status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                // Log exception
                logger.LogError(ex, "Error adding product");
                throw;
            }
        }
    }
}
