using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Shop.Shared.Entities
{
    /// <summary>
    ///     Represents a product category in the shop.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        ///     Gets or sets the unique identifier for the product category.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the product category.
        /// </summary>
        public string Name { get; set; }
    }
}
