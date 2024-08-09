namespace ECommerceWebAPI.DTO.Products
{
    public class AddProductToWishlistDTO
    {

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public bool InWishlist { get; set; }
    }
}
