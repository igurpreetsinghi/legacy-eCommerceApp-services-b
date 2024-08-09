using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Users;

namespace ECommerceWebAPI.Interfaces
{
    public interface IAuthService
    {
        Task<bool?> SignUpAsync(CreateUserDTO authsignUpDTO);
        public Task<SignUpDto?> LoginAsync(SignUpDto authloginDTO);
    }
}