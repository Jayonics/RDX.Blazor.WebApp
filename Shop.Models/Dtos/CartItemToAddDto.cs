namespace Shop.Models.Dtos
{
    public class CartItemToAddDto
    {
        public int CardId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
