namespace ECommerceWebAPI.DTO.Products
{
    public class CheckIsProductAvailableInWishlistDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public bool InWishlist { get; set; }
    }
}
