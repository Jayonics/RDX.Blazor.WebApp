namespace Shop.Shared.Entities
{
    /// <summary>
    ///     Represents an item in a shopping cart.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        ///     Gets or sets the unique identifier for the cart item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the cart that the item belongs to.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the product that the cart item represents.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        ///     Gets or sets the quantity of the product in the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
