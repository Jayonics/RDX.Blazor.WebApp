using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class ProductImageRequest
    {
        // The ID may be required for updating the image, but it is not required for creating a new image.
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        // The Name is required for identifying the image (path and name of the file).
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        // The ContentMD5 is an optional field that can be used for repairing the Name identifier to a file by searching for the hash in the storage container.
        public byte[]? ContentMD5 { get; set; }

    }
}
