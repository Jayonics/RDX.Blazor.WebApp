using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shop.Shared.Entities
{
    /// <summary>
    /// Represents a Service in the Shop
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or sets the unique identifier for the service.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the service.
        /// This is a SQL Server specific data type that maps to the .NET decimal type.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of products that the service can be performed on.
        /// </summary>
        public ICollection<Product>? Products { get; set; }
    }
}