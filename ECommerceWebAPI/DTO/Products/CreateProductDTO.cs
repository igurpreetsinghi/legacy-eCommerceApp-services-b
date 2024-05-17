using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Products
{
    public class CreateProductDTO
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public List<IFormFile> Pictures { get; set; }
    }
}
