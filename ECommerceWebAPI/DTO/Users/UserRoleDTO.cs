using ECommerceWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.DTO.Users
{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
