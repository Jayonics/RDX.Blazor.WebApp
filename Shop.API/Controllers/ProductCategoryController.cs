using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.API.Extensions;
using Shop.API.Repositories.Contracts;
using Shop.Models.Dtos;

namespace Shop.API.Controllers
{
    /// <summary>
    /// Controller for handling product category-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryController"/> class.
        /// </summary>
        /// <param name="productCategoryRepository">The product category repository.</param>
        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            this._productCategoryRepository = productCategoryRepository;
        }

        /// <summary>
        /// Gets all categories from the product category repository.
        /// </summary>
        /// <returns>A list of product category DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            // Try to execute the following code
            try
            {
                // Fetch all product categories from the repository
                var productCategories = await this._productCategoryRepository.GetProductCategories();

                // If the product categories are null, return a NotFound status
                if (productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    // Convert the product categories to DTOs
                    var productCategoryDtos = productCategories.ConvertToDto();

                    // Return the converted DTOs with an Ok status
                    return Ok(productCategoryDtos);
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

        /// <summary>
        /// Gets a single category from the product category repository.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A product category DTO.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategory(int id)
        {
            // Try to execute the following code
            try
            {
                // Fetch the product category from the repository
                var productCategory = await this._productCategoryRepository.GetProductCategory(id);

                // If the product category is null, return a NotFound status
                if (productCategory == null)
                {
                    return NotFound();
                }
                else
                {
                    // Convert the product category to a DTO
                    var productCategoryDto = productCategory.ConvertToDto();

                    // Return the converted DTO with an Ok status
                    return Ok(productCategoryDto);
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
    }
}
