using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region Category

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDTO addCategoey)
        {
            try
            {
                var response = await _adminService.AddCategory(addCategoey);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            try
            {
                var response = await _adminService.GetByCategoryId(id);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody] UpdateCategoryDTO editCategory)
        {
            try
            {
                var response = await _adminService.EditCategory(editCategory);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var response = await _adminService.DeleteCategory(id);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchKeyword = "")
        {
            try
            {
                var response = await _adminService.SearchCategory(pageNumber, pageSize, searchKeyword);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var response = await _adminService.GetAllCategory();
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion


        #region Products

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDTO productData)
        {
            try
            {
                if (productData != null)
                {
                    var response = await _adminService.AddProduct(productData);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(new { errormessage = "Something Went Wrong Try Again Later." });
                    }
                }
                else
                {
                    return BadRequest(new { errormessage = "All fields are required." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductId(int id)
        {
            try
            {
                var response = await _adminService.GetByProductId(id);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct([FromForm] UpdateProductDTO productData)
        {
            try
            {
                if (productData != null)
                {
                    var response = await _adminService.EditProduct(productData);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(new { errormessage = "Something Went Wrong Try Again Later." });
                    }
                }
                else
                {
                    return BadRequest(new { errormessage = "All fields are required." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var response = await _adminService.DeleteProduct(id);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchKeyword = "", [FromQuery] int categoryId = 0)
        {
            try
            {
                var response = await _adminService.SearchProducts(pageNumber, pageSize, searchKeyword, categoryId);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Products

        #region User Management


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                if ((bool)await _adminService.AddUser(createUserDTO))
                {
                    return Ok("User registered successfully.");
                }
                return Problem("User already exists.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(int id)
        {
            try
            {
                var response = await _adminService.GetByUserId(id);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                if ((bool)await _adminService.EditUser(updateUserDTO))
                {
                    return Ok("User Profile Updated successfully.");
                }
                return Problem("Something went wrong try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> UpdateUserStatus(int id)
        {
            try
            {
                if ((bool)await _adminService.ChangeUserStatus(id))
                {
                    return Ok("The User's status has been successfully updated.");
                }
                return Problem("Something went wrong try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchKeyword = "")
        {
            try
            {
                var response = await _adminService.SearchUsers(pageNumber, pageSize, searchKeyword);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmailId([FromQuery] string email)
        {
            try
            {
                var response = await _adminService.GetUserByEmailId(email);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var response = await _adminService.GetUserRoles();
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new { errormessage = "Something went wrong try again later." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        #endregion User Management


    }
}
