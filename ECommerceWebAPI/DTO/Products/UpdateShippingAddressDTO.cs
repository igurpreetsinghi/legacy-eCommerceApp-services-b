using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.DTO.Products
{
    public class UpdateShippingAddressDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string? Gender { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Phone { get; set; }

        public string? Country { get; set; }

        public string? PINCode { get; set; }
    }
}
