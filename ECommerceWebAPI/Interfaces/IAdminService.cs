using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.Interfaces
{
    public interface IAdminService
    {

        #region Category Management

        Task<CreateCategoryDTO?> AddCategory(CreateCategoryDTO addCategory);

        Task<UpdateCategoryDTO> EditCategory(UpdateCategoryDTO editCategoryData);

        Task<UpdateCategoryDTO> GetByCategoryId(int categoryId);

        Task<bool> DeleteCategory(int id);

        Task<PagedResponse<Category>> SearchCategory(int pageNumber, int pageSize, string searchKeyword);

        Task<List<UpdateCategoryDTO>> GetAllCategory();

        #endregion Category Management

        #region Product Management
        Task<CreateProductDTO?> AddProduct(CreateProductDTO productData);

        Task<GetProductDTO> GetByProductId(int productId);

        Task<UpdateProductDTO> EditProduct(UpdateProductDTO editProductData);

        Task<bool> DeleteProduct(int id);

        Task<PagedResponse<GetProductDTO>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId);

        #endregion Management

        #region User Management

        Task<bool?> AddUser(CreateUserDTO addUser);

        Task<GetUserDTO> GetByUserId(int userId);

        Task<bool?> EditUser(UpdateUserDTO editUserData);

        Task<bool?> ChangeUserStatus(int id);

        Task<PagedResponse<GetUserListDTO>> SearchUsers(int pageNumber, int pageSize, string searchKeyword);

        Task<GetUserDTO> GetUserByEmailId(string emailId);

        Task<List<GetUserRoleDTO>> GetUserRoles();

        #endregion Management
    }
}