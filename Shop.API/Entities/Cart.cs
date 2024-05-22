namespace Shop.API.Entities
{
    /// <summary>
    ///     Represents a shopping cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        ///     Gets or sets the unique identifier for the cart.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the user that the cart belongs to.
        /// </summary>
        public int UserId { get; set; }
    }
}
