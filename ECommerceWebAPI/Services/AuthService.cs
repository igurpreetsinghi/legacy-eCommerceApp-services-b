using AutoMapper;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace ECommerceWebAPI.Services
{
    public class AuthService : IAuthService
    {
        #region Fields

        private readonly DataContext _context;
        private IMapper _mapper;
        private IConfiguration _config;

        #endregion Fields

        #region Ctor

        public AuthService(DataContext dataContext, IMapper mapper, IConfiguration config)
        {

            _context = dataContext;
            _mapper = mapper;
            _config = config;
        }

        #endregion Ctor
        public async Task<bool?> SignUpAsync(CreateUserDTO addUser)
        {
            try
            {
                if (addUser == null) return null;

                if (await _context.tbl_User.AnyAsync(u => u.UserName == addUser.UserName || u.Email == addUser.Email))
                {
                    return false; // User already exists
                }

                User user = _mapper.Map<User>(addUser);
                user.Password= BCrypt.Net.BCrypt.HashPassword(addUser.Password);
                user.IsActive = true;
                user.CustomerGuid = Guid.NewGuid();
                user.CreatedDate = DateTime.Now;
                await _context.tbl_User.AddAsync(user);
                await _context.SaveChangesAsync();

                var URole = await _context.tbl_User.Where(_ => _.CustomerGuid == user.CustomerGuid).FirstOrDefaultAsync();
                if (URole == null) { return null; }
                UserRole role = new UserRole();
                role.RoleId = addUser.RoleId;
                role.UserId = URole.Id;
                role.CreatedDate = DateTime.Now;
                await _context.tbl_UserRole.AddAsync(role);
                await _context.SaveChangesAsync();

                var UserDTO = _mapper.Map<CreateUserDTO>(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SignUpDto?> LoginAsync(SignUpDto authsignUpDTO)
        {
            try
            {
                var user = await _context.tbl_User.SingleOrDefaultAsync(u => u.Email == authsignUpDTO.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(authsignUpDTO.Password, user.Password))
                {
                    return null; // Invalid username or password
                }

                return authsignUpDTO;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}