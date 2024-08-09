using ECommerceWebAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebAPI.DTO.Users
{
    public class GetUserListDTO
    {
        public int Id { get; set; }

        public Guid CustomerGuid { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }
    }
}
