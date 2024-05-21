using Shop.API.Entities;

namespace Shop.API.Repositories.Contracts
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<ProductCategory> GetProductCategory(int id);
        Task<ProductCategory> AddProductCategory(ProductCategory productCategory);
        Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory);
        Task<bool> DeleteProductCategory(int id);
    }
}
