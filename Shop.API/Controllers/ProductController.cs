using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.API.Extensions;
using Shop.API.Repositories.Contracts;
using Shop.Models.Dtos;
using Shop.Models.Requests;
using Shop.Shared.Entities;
using Shop.Shared.Extensions;

namespace Shop.API.Controllers
{
    /// <summary>
    ///     Controller for handling product-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly ILogger<ProductController> _logger;

        readonly IProductRepository _productRepository;

        private readonly IMediaRepository _mediaRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProductController" /> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger, IMediaRepository mediaRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mediaRepository = mediaRepository;
        }

        /// <summary>
        ///     Gets all items from the product repository.
        /// </summary>
        /// <returns>A list of product DTOs.</returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            // Try to execute the following code
            try
            {
                // Fetch all products from the repository
                var products = await _productRepository.GetProducts();

                // If either products or product categories are null, return a NotFound status
                if (!products.Any())
                {
                    return NotFound();
                }
                // Convert the products to DTOs using the fetched categories
                var productDtos = products.ConvertToDto();

                // Return the converted DTOs with an Ok status
                return Ok(productDtos);
            }
            // If any exception occurs during the execution of the try block
            catch (Exception)
            {
                // Return a 500 Internal Server Error status with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null) return NotFound();

            var productDto = product.ConvertToDto();
            return Ok(productDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromRoute] int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var product = productDto.ConvertToEntity();

            try
            {
                product = await _productRepository.UpdateProduct(product);

                // Attempt to remove any existing ProductImages on the Product, then add the new ProductImage
                try
                {
                    var productImages = await _mediaRepository.GetProductImagesAsync(product.Id);
                    _logger.LogInformation($"Found {productImages.Count()} ProductImages for Product {product.Id}");
                    if (productImages.Any())
                    {
                        _logger.LogInformation($"Attempting to delete {productImages.Count()} ProductImages for Product {product.Id}");
                        await _mediaRepository.DeleteProductImagesAsync(product.Id);
                        _logger.LogInformation($"Deleted {productImages.Count()} ProductImages for Product {product.Id}");
                    }
                    // Then add the new ProductImage
                    if (productDto.Image != null)
                    {
                        var productImageRequest = new ProductImage
                        {
                            ProductId = product.Id,
                            Name = productDto.Image.Name,
                            ContentMD5 = productDto.Image.ContentMD5 ?? null
                        };
                        var productImage = await _mediaRepository.AddProductImageAsync(productImageRequest);
                        _logger.LogInformation($"Added ProductImage {productImage.Id} to Product {product.Id}");
                        _logger.LogInformation($"ProductImage Name: {productImage.Name}");
                    }
                }
                catch
                {
                    // If no ProductImages are found, add a new ProductImage
                    if (productDto.Image != null)
                    {
                        var productImageRequest = new ProductImage
                        {
                            ProductId = product.Id,
                            Name = productDto.Image.Name,
                            ContentMD5 = productDto.Image.ContentMD5 ?? null
                        };
                        var productImage = await _mediaRepository.AddProductImageAsync(productImageRequest);
                        _logger.LogInformation($"Added ProductImage {productImage.Id} to Product {product.Id}");
                    }
                }


                product = await _productRepository.GetProduct(product.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                    return NotFound();
                throw;
            }

            var updatedProductDto = product.ConvertToDto();
            return Ok(updatedProductDto);
        }

        async Task<bool> ProductExists([FromRoute] int id)
        {
            var product = await _productRepository.GetProduct(id);
            return product != null;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null) return NotFound();

            try
            {
                await _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data from the database");
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] NewProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Attempt to convert the DTO to an entity before adding it to the repository


            // Return a 400 Bad Request status if the conversion fails
            if (productDto.ConvertToEntity() != null)
                try
                {
                    var product = await _productRepository.AddProduct(productDto.ConvertToEntity());
                    if (productDto.Image != null)
                    {
                        // Perform the conversion of the ProductImageRequest to the ProductImage entity.
                        var productImageRequest = new ProductImage
                        {
                            ProductId = product.Id,
                            Name = productDto.Image.Name,
                            ContentMD5 = productDto.Image.ContentMD5 ?? null
                        };
                        var productImage = await _mediaRepository.AddProductImageAsync(productImageRequest);
                    }
                    product = await _productRepository.GetProduct(product.Id);

                    return Ok(product.ConvertToDto());

                }
                catch (Exception exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    exception.Message);
                }
            return BadRequest(productDto);
        }
    }
}
