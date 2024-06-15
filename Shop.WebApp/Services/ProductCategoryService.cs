using Shop.Models.Dtos;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        readonly HttpClient httpClient;

        readonly ILogger<ProductCategoryService> logger;

        public ProductCategoryService(HttpClient httpClient, ILogger<ProductCategoryService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            var productCategories = await httpClient.GetFromJsonAsync<IEnumerable<ProductCategoryDto>>("api/ProductCategory");
            return productCategories;
        }
        public async Task<ProductCategoryDto> GetProductCategory(int id)
        {
            var productCategory = await httpClient.GetFromJsonAsync<ProductCategoryDto>($"api/ProductCategory/{id}");
            if (productCategory == null) throw new Exception("Product category not found");
            return productCategory;
        }
    }
}
