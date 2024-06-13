using Microsoft.AspNetCore.Components.Authorization;
using Serilog;
using Shop.Models.Dtos;
using Shop.Models.Requests;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp.Services
{
    public class ProductService : IProductService
    {
        readonly HttpClient _httpClient;
        readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            this._httpClient = httpClient;
            this._logger = logger;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
            return products;
        }
        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductDto>($"api/Product/{id}");
            if (product == null) throw new Exception("Product not found");
            return product;
        }

        public async Task<ProductDto> UpdateProduct(ProductDto product)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Product/{product.Id}", product);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                // You can throw an exception or return null based on your error handling strategy
                throw new Exception($"Product update failed with status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                _logger.LogDebug("DeleteProduct: {id}", id);

                var response = await _httpClient.DeleteAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Product deleted successfully {response.StatusCode}");
                    return true;
                }
                _logger.LogWarning($"Product delete failed with status code {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, "Error deleting product");
                throw;
            }
        }

        public async Task<ProductDto> AddProduct(NewProductDto product)
        {
            try
            {
                _logger.LogDebug("AddProduct: {product}", product);

                var response = await _httpClient.PostAsJsonAsync("api/Product", product);
                var returnProduct = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Return product: {returnProduct}");

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Product added successfully {response.StatusCode}");
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                _logger.LogWarning($"Product add failed with status code {response.StatusCode}");
                // You can throw an exception or return null based on your error handling strategy
                throw new Exception($"Product add failed with status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, "Error adding product");
                throw;
            }
        }
    }
}
