using Shop.Models.Dtos;

namespace Shop.WebApp.Services.Contracts
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<ProductCategoryDto> GetProductCategory(int id);
    }
}
