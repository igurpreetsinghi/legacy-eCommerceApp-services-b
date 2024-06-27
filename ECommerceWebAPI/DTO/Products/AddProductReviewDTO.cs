namespace ECommerceWebAPI.DTO.Products
{
    public class AddProductReviewDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int OrderItemId { get; set; }

        public int Rating { get; set; }

        public IFormFile? Pictures { get; set; }
    }
}
