using Shop.Models.Dtos;

namespace Shop.WebApp.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(int id);
        Task<bool> UpdateProduct(ProductDto product);
    }
}
