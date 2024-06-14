using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Products
{
    public class YourWishlistDTO
    {
        public int Id { get; set; }

        public int WishlistId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public byte[]? ImageData { get; set; }


    }
}
