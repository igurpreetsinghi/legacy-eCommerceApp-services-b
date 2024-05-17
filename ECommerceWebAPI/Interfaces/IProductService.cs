using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.Interfaces
{
    public interface IProductService
    {
        Task<CreateProductDTO?> AddProduct(CreateProductDTO productData);

        Task<GetProductDTO> GetByProductId(int productId);

        Task<UpdateProductDTO> EditProduct(UpdateProductDTO editCategoryData);

        Task<bool> DeleteProduct(int id);

        Task<PagedResponse<Product>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId);
        Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId);
    }
}