using Microsoft.AspNetCore.Mvc;
using Shop.Models.Dtos;
using Shop.WebApp.Services.Contracts;


namespace Shop.WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ProductService> logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var products = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                return products;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
        public async Task<ProductDto> GetProduct(int id)
        {
            try
            {
                var product = await this.httpClient.GetFromJsonAsync<ProductDto>($"api/Product/{id}");
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                return product;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        public async Task<ProductDto> UpdateProduct(ProductDto product)
        {
            try
            {
                var response = await this.httpClient.PutAsJsonAsync($"api/Product/{product.Id}", product);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Product updated successfully {response.StatusCode}");
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    logger.LogWarning($"Product update failed with status code {response.StatusCode}");
                    // You can throw an exception or return null based on your error handling strategy
                    throw new Exception($"Product update failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Log exception
                logger.LogError(ex, "Error updating product");
                throw;
            }
        }
    }
}
