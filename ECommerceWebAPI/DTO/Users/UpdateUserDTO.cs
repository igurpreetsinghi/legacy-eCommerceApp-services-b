using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Users
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }

        public string? RoleName { get; set; }


        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        //[Required]  
        //public List<IFormFile> Pictures { get; set; }
    }
}
