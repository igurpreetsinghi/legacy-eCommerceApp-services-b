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
    public class ProductController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IProductService _productService;

        #region Products
        public ProductController(IAdminService adminService, IProductService productService)
        {
            _adminService = adminService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchKeyword = "", [FromQuery] int categoryId = 0)
        {
            try
            {
                var response = await _productService.SearchProducts(pageNumber, pageSize, searchKeyword, categoryId);
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
                var response = await _productService.GetAllCategory();
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
        public async Task<IActionResult> GetByProductId(int productId)
        {
            try
            {
                var response = await _productService.GetByProductId(productId);
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

        #endregion Product 

        #region Product Reviews

        [HttpGet]
        public async Task<IActionResult> GetProductReview(int productId)
        {
            try
            {
                var response = await _productService.GetProductReviewByProductId(productId);
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

        #endregion Product Reviews

        #region Product Wishlist

        [HttpPost]
        public async Task<IActionResult> AddProductToWishlist([FromBody] AddProductToWishlistDTO addProductWishlist)
        {
            try
            {
                var response = await _productService.AddProductToWishlist(addProductWishlist);
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
        public async Task<IActionResult> YourWishlist(int UserId)
        {
            try
            {
                var response = await _productService.YourWishlist(UserId);
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
        public async Task<IActionResult> RemoveProductWishlist(int id)
        {
            try
            {
                if ((bool)await _productService.RemoveProductWishlist(id))
                {
                    return Ok("Removed Product from Wishlist.");
                }
                return Problem("Something went wrong try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Product Wishlist

        #region Shipping Address

        [HttpPost]
        public async Task<IActionResult> AddShippingAddress([FromBody] AddShippingAddressDTO addShippingAddress)
        {
            try
            {
                if ((bool)await _productService.AddShippingAddress(addShippingAddress))
                {
                    return Ok("Address added successfully.");
                }
                return Problem("Address already exists.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetShippingAddressByUserId(int UserId)
        {
            try
            {
                var response = await _productService.GetShippingAddressByUserId(UserId);
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
        public async Task<IActionResult> EditShippingAddress([FromBody] UpdateShippingAddressDTO updateAddressDTO)
        {
            try
            {
                if ((bool)await _productService.EditShippingAddress(updateAddressDTO))
                {
                    return Ok("Shipping Address Updated Successfully.");
                }
                return Problem("Something went wrong try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Shipping Address

        #region Cart

        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartDTO addToCart)
        {
            try
            {
                if ((bool)await _productService.AddProductToCart(addToCart))
                {
                    return Ok("Product added to Cart Successfully.");
                }
                return Problem("Product already exists.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartByUserId(int UserId)
        {
            try
            {
                var response = await _productService.GetCartByUserId(UserId);
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
        public async Task<IActionResult> EditCartItemQuantity([FromBody] UpdateCartItemQuantityDTO editCartQuantityData)
        {
            try
            {
                if ((bool)await _productService.EditCartItemQuantity(editCartQuantityData))
                {
                    return Ok("Cart Item Quantity Updated Successfully.");
                }
                return Problem("Something went wrong try again later.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        #endregion Cart



    }
}