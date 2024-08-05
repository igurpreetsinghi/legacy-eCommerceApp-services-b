using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Products
{
    public class GetYourOrderDTO
    {
        public int OrderId { get; set; }

        public Guid OrderRefNo { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }
        public int OrderItemId { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }

        public byte[]? ImageData { get; set; }

        
    }
}
