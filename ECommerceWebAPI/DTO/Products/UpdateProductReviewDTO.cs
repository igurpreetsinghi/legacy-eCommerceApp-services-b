namespace ECommerceWebAPI.DTO.Products
{
    public class UpdateProductReviewDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int OrderItemId { get; set; }

        public int Rating { get; set; }

        public IFormFile? Pictures { get; set; }
    }
}
