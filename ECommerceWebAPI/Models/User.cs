using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid CustomerGuid { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string? UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Required]
        public Address? Address { get; set; }
        public int AddressId { get; set; }

        [Required]
        public List<OrderItem>? OrderItems { get; set; }

        [Required]
        public List<ShoppingCart>? ShoppingCart { get; set; }

        [Required]
        public List<UserRole>? UserRoles { get; set; }
    }
}
