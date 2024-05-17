using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.DTO
{
    public class SignUpDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}