namespace ECommerceWebAPI.DTO
{
    public class PictureDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? ProductReviewId { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}