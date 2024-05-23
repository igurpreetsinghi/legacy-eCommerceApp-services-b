using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.DTO
{
    public class SignUpDto
    {

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}