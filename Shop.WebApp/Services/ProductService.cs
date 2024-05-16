using Shop.Models.Dtos;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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

        public async Task<bool> UpdateProduct(ProductDto product)
        {
            try
            {
                var response = await this.httpClient.PutAsJsonAsync($"api/Product/{product.Id}", product);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
    }
}
