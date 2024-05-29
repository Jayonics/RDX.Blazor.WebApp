using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Shop.Shared.Entities
{
    public enum StorageContainers
    {
        shop = 0
    }

    /// <summary>
    /// Represents an image associated with a product in the shop.
    /// </summary>
    public class ProductImage
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product image.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the relationship with the product.
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Navigation property for the relationship with the product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the path and name of the file (including the file extension).
        /// Note: This does not include the URL of Storage Container.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hash of the file (MD5).
        /// This can be used for repairing the Name identifier to a file by searching for the hash in the storage container.
        /// </summary>
        public byte[]? ContentMD5 { get; set; }

        /// <summary>
        /// Gets or sets a reference to the storage container (potentially multiple) if the file is stored in Azure Blob Storage.
        /// </summary>
        public IList<StorageContainers> StorageContainers { get; set; }
    }
}