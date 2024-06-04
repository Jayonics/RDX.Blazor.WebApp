using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Models.Requests;
using Shop.Models.Responses;
using Shop.Shared.Entities;

namespace Shop.API.Repositories.Contracts
{
    public interface IMediaRepository
    {
        Task<IEnumerable<ProductImage>> GetAllProductImagesAsync();
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);
        Task<ProductImage> GetProductImageAsync(int id);
        Task<ProductImage> UpdateProductImageAsync(ProductImage productImage);
        Task<bool> DeleteProductImageAsync(int id);
        Task<ProductImage> AddProductImageAsync(ProductImage productImage);
        Task<bool> DeleteProductImagesAsync(int productId);
    }
}
