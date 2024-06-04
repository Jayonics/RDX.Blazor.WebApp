using Shop.Shared.Entities;
using Shop.Models.Dtos;
using Shop.Models.Requests;
using Shop.Models.Responses;
using Shop.Shared.Extensions;

namespace Shop.API.Extensions
{
    /// <summary>
    ///     Provides extension methods for converting entities to DTOs.
    /// </summary>
    public static class DtoConversions
    {
        /// <summary>
        ///     Converts a collection of Product entities to ProductDto objects.
        /// </summary>
        /// <param name="products">The collection of Product entities to convert.</param>
        /// <returns>A collection of ProductDto objects.</returns>
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products) => (from product in products
                                                                select new ProductDto
                                                                {
                                                                    Id = product.Id,
                                                                    Name = product.Name,
                                                                    Description = product.Description,
                                                                    ImageURL = product.ImageURL,
                                                                    Price = product.Price,
                                                                    Quantity = product.Quantity,
                                                                    CategoryId = product.CategoryId,
                                                                    CategoryName = product.Category.Name
                                                                }).ToList();

        /// <summary>
        ///     Converts a single Product entity to a ProductDto object.
        /// </summary>
        /// <param name="product">The Product entity to convert.</param>
        /// <returns>A ProductDto object.</returns>
        public static ProductDto ConvertToDto(this Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        };

        public static Product ConvertToEntity(this ProductDto productDto) => new()
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            ImageURL = productDto.ImageURL,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
            CategoryId = productDto.CategoryId
        };

        public static Product ConvertToEntity(this NewProductDto productDto) => new()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
            CategoryId = productDto.CategoryId
        };

        // ProductCategory conversions
        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories) => (from productCategory in productCategories
                                                                                                                            select new ProductCategoryDto
                                                                                                                            {
                                                                                                                                Id = productCategory.Id,
                                                                                                                                Name = productCategory.Name
                                                                                                                            }).ToList();

        public static ProductCategoryDto ConvertToDto(this ProductCategory productCategory) => new()
        {
            Id = productCategory.Id,
            Name = productCategory.Name
        };

        public static ProductCategory ConvertToEntity(this ProductCategoryDto productCategoryDto) => new()
        {
            Id = productCategoryDto.Id,
            Name = productCategoryDto.Name
        };
    }
}
