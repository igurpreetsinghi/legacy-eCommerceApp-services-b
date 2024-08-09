using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Products
{
    public class UpdateCartItemQuantityDTO
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
