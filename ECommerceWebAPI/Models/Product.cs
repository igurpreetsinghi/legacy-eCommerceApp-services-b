using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        [Required]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public byte[]? ImageData { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        [Required]
        public List<OrderItem>? OrderItems { get; set; }

        [Required]
        public List<ShoppingCart>? ShoppingCart { get; set; }

        [Required]
        public List<ProductReview>? ProductReview { get; set; }

        [Required]
        public List<ProductWishlist>? ProductWishlist { get; set; }
    }
}
