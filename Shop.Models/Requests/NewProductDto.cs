using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class NewProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        // // The Image is now a separate entity, so we don't need to include the URL here.
        // public string ImageURL { get; set; }
        public ProductImageRequest? Image { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 1")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }
    }
}
