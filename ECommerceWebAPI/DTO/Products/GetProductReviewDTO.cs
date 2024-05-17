namespace ECommerceWebAPI.DTO.Products
{
    public class GetProductReviewDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
