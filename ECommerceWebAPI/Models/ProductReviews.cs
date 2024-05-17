using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_ProductReview")]
    public class ProductReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public Product? Product { get; set; }
        public int ProductId { get; set; }

        [Required]
        public User? User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public List<Pictures>? Pictures { get; set; }
    }
}