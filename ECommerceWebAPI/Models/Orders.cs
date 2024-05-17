using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_Orders")]
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid OrderGuid { get; set; }

        public int CustomerId { get; set; }

        public int BillingAddressId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTax { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderDiscount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotal { get; set; }
        [Required]
        public List<OrderItem>? OrderItems { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
