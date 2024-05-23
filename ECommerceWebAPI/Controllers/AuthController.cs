using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Models;
using ECommerceWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ECommerceWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] CreateUserDTO addUser)
        {
            try
            {
                if ((bool)await _authService.SignUpAsync(addUser))
                {
                    return Ok("User registered successfully.");
                }
                return BadRequest("User already exists.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] SignUpDto authsignUpDTO)
        {
            try
            {
                var user = await _authService.LoginAsync(authsignUpDTO);
                if (user != null)
                {
                    return Ok("Login successful.");
                }
                return Unauthorized("Invalid username or password.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }

}