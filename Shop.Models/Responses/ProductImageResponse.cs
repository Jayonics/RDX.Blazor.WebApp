using System.ComponentModel.DataAnnotations;
using Shop.Models.Dtos;

namespace Shop.Models.Responses
{
    public class ProductImageResponse
    {
        // The ID is required for identifying the image.
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }
        // The ProductId is required for associating the image with a product.
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }
        // The Name is required for identifying the image (path and name of the file).
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        // The ContentMD5 is an optional field that can be used for repairing the Name identifier to a file by searching for the hash in the storage container.
        public byte[]? ContentMD5 { get; set; }
        // The ProductImageResponse may optionally include the BlobResponseDto for the image.
        public BlobResponseDto? Blob { get; set; }
        // The ProductImageResponse may optionally include the full URL for the image.
        public string? Url { get; set; }
    }
}
