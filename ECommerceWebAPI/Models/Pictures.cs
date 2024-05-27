using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_Pictures")]
    public class Pictures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        //public Product? Product { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public ProductReview? ProductReview { get; set; }
        public int? ProductReviewId { get; set; }
        public byte[]? ImageData { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
