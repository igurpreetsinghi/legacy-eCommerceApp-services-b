using ECommerceWebAPI.Interfaces;
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

        #region Wishlist



        #endregion
    }
}
