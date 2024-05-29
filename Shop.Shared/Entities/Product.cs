using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shop.Shared.Entities
{
    /// <summary>
    ///     Represents a product in the shop.
    /// </summary>
    public class Product
    {
        /// <summary>
        ///     Gets or sets the unique identifier for the product.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     This method is used to get the URL of the image for the product from the storage container.
        /// </summary>
        [NotMapped]
        public string ImageURL { get; set; }

        /// <summary>
        ///  Gets or sets the collection of images for the product.
        /// </summary>
        public ICollection<ProductImage>? Images { get; set; }

        /// <summary>
        ///     Gets or sets the price of the product.
        /// </summary>

        [Column(TypeName = "money")]// This is a SQL Server specific data type that maps to the .NET decimal type.
        public decimal Price { get; set; }

        /// <summary>
        ///     Gets or sets the quantity of the product in stock.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the category that the product belongs to.
        /// </summary>
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Navigation property for the relationship with the category.
        /// </summary>
        public ProductCategory Category { get; set; }
    }
}
