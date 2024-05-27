using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<Product>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId);

        Task<List<CreateCategoryDTO>> GetAllCategory();

        Task<GetProductDTO> GetByProductId(int productId);
        Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId);
    }
}