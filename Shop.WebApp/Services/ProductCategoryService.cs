using Shop.Models.Dtos;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly HttpClient httpClient;

        private readonly ILogger<ProductCategoryService> logger;

        public ProductCategoryService(HttpClient httpClient, ILogger<ProductCategoryService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var productCategories = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductCategoryDto>>("api/ProductCategory");
                return productCategories;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
        public async Task<ProductCategoryDto> GetProductCategory(int id)
        {
            try
            {
                var productCategory = await this.httpClient.GetFromJsonAsync<ProductCategoryDto>($"api/ProductCategory/{id}");
                if (productCategory == null)
                {
                    throw new Exception("Product category not found");
                }
                return productCategory;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
    }
}
