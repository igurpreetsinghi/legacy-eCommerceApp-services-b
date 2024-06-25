namespace ECommerceWebAPI.DTO.Products
{
    public class AddPlaceOrderItemDTO
    {

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
