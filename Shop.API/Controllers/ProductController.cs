using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.API.Extensions;
using Shop.API.Repositories.Contracts;
using Shop.Models.Dtos;

namespace Shop.API.Controllers
{
    /// <summary>
    /// Controller for handling product-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Gets all items from the product repository.
        /// </summary>
        /// <returns>A list of product DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            // Try to execute the following code
            try
            {
                // Fetch all products from the repository
                var products = await this.productRepository.GetProducts();
                // Fetch all product categories from the repository
                var productCategories = await this.productRepository.GetCategories();

                // If either products or product categories are null, return a NotFound status
                if (products == null || productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    // Convert the products to DTOs using the fetched categories
                    var productDtos = products.ConvertToDto(productCategories);
                    // Return the converted DTOs with an Ok status
                    return Ok(productDtos);
                }
            }
            // If any exception occurs during the execution of the try block
            catch (Exception)
            {
                // Return a 500 Internal Server Error status with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await this.productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            var productCategory = await this.productRepository.GetCategory(product.CategoryId);
            if (productCategory == null)
            {
                return NotFound();
            }

            var productDto = product.ConvertToDto(productCategory);
            return Ok(productDto);
        }
        private async Task<bool> ProductExists(int id)
        {
            var product = await productRepository.GetProduct(id);
            return product != null;
        }
    }   
}
