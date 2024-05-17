using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.Models
{

    [Table("tbl_User_Role_Intermidate")]
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Role? Role { get; set; }
        public int RoleId { get; set; }

        [Required]
        public User? User { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
