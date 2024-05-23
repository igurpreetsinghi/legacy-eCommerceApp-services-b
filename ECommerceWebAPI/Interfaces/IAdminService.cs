using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.Interfaces
{
    public interface IAdminService
    {
        #region Product
        Task<CreateProductDTO?> AddProduct(CreateProductDTO productData);

        Task<GetProductDTO> GetByProductId(int productId);

        Task<UpdateProductDTO> EditProduct(UpdateProductDTO editProductData);

        Task<bool> DeleteProduct(int id);

        Task<PagedResponse<Product>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId);

        #endregion

        #region Category

        Task<CategoryDTO?> AddCategory(CategoryDTO addCategory);

        Task<CategoryDTO> EditCategory(CategoryDTO editCategoryData);

        Task<CategoryDTO> GetByCategoryId(int categoryId);

        Task<bool> DeleteCategory(int id);

        Task<PagedResponse<Category>> SearchCategory(int pageNumber, int pageSize, string searchKeyword);

        Task<List<CategoryDTO>> GetAllCategory();

        #endregion

        #region User

        Task<CreateUserDTO?> AddUser(CreateUserDTO addCategory);

        Task<UpdateUserDTO> EditUser(UpdateUserDTO editCategoryData);

        Task<GetUserDTO> GetByUserId(int categoryId);

        Task<bool> ChangeUserStatus(int id);

        Task<PagedResponse<User>> SearchUsers(int pageNumber, int pageSize, string searchKeyword);

        Task<GetUserDTO> GetUserByEmailId(string emailId);

        Task<List<GetUserRoleDTO>> GetUserRoles();

        #endregion
    }
}