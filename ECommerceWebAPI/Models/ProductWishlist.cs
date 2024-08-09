using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_ProductWishlist")]
    public class ProductWishlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool InWishlist { get; set; }

        [Required]
        public Product? Product { get; set; }
        public int ProductId { get; set; }

        [Required]
        public User? User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
