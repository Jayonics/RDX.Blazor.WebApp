using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class NewProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Url(ErrorMessage = "Invalid URL")]
        [Required(ErrorMessage = "Image URL is required")]
        public string ImageURL { get; set; }
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
