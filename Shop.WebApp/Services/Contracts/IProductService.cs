using Shop.Models.Dtos;

namespace Shop.WebApp.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}
