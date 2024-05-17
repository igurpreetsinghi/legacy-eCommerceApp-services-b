using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerceWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private IConfiguration _config;

        public AuthController(IAdminService adminService, IConfiguration config)
        {
            _adminService = adminService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] CreateUserDTO login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var token = GenerateToken();
                response = Ok(new JWTTokenResponse
                {
                    Token = token
                });
            }

            return response;
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:IssuerSigningKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:ValidIssuer"],
                audience: _config["Jwt:ValidAudience"],
                claims: null,
                notBefore: null,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:TokenExpiresInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private CreateUserDTO AuthenticateUser(CreateUserDTO login)
        {
            CreateUserDTO user = null;

            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information
            if (login.UserName == "Jignesh")
            {
                user = new CreateUserDTO { UserName = "Jignesh Trivedi", Email = "test.btest@gmail.com" };
            }
            return user;
        }
    }

}