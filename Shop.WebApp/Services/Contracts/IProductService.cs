using Shop.Models.Dtos;
using Shop.Models.Requests;

namespace Shop.WebApp.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> UpdateProduct(ProductDto product);
        Task<bool> DeleteProduct(int id);
        Task<ProductDto> AddProduct(NewProductDto product);
    }
}
