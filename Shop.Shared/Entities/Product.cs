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
        ///     Gets or sets the URL of the product's image.
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        ///     Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Gets or sets the quantity of the product in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the category that the product belongs to.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
