namespace ECommerceWebAPI.DTO.Products
{
    public class AddProductToCartDTO
    {

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }
    }
}
