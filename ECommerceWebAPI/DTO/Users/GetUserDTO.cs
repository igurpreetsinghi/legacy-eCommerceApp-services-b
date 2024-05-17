using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Users
{
    public class GetUserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
