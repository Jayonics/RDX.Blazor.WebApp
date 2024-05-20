using Shop.API.Entities;

namespace Shop.API.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetProduct(int id);
        Task<ProductCategory> GetCategory(int id);
        Task<Product> UpdateProduct(Product product);
    }
}
