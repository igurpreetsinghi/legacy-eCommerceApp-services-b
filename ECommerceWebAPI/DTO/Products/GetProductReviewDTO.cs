namespace ECommerceWebAPI.DTO.Products
{
    public class GetProductReviewDTO
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int Rating { get; set; }

        public byte[]? ImageData { get; set; }
        public DateTime ReviewedDate { get; set; }
    }
}
