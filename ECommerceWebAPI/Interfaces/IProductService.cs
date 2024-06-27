using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;
using System.Threading.Tasks;

namespace ECommerceWebAPI.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<GetProductDTO>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId);

        Task<List<UpdateCategoryDTO>> GetAllCategory();

        Task<GetProductDTO> GetByProductId(int productId);

        Task<AddProductToWishlistDTO?> AddProductToWishlist(AddProductToWishlistDTO addProductWishlist);

        Task<List<YourWishlistDTO>> YourWishlist(int UserId);

        Task<bool?> RemoveProductWishlist(int id);

        #region Shipping Address

        Task<bool?> AddShippingAddress(AddShippingAddressDTO addShippingAddress);
        Task<GetShippingAddressDTO> GetShippingAddressByUserId(int UserId);
        Task<bool?> EditShippingAddress(UpdateShippingAddressDTO editAddressData);

        #endregion Shipping Address

        #region Cart

        Task<bool?> AddProductToCart(AddProductToCartDTO addToCart);
        Task<List<GetYourCartDTO>> GetCartByUserId(int UserId);

        Task<bool?> EditCartItemQuantity(UpdateCartItemQuantityDTO editCartQuantityData);
        Task<bool?> DeleteItemFromYourCart(int id);

        #endregion Cart

        #region Order

        Task<bool?> PlaceOrder(AddPlaceOrderDTO productOrderData);

        Task<List<GetYourOrderDTO>> GetYourOrder(int UserId);

        #endregion Order

        #region Ratings & Reviews

        Task<bool?> AddRatingReviews(AddProductReviewDTO addProductReview);

        Task<bool?> EditRatingReviews(UpdateProductReviewDTO editProductReviewData);

        Task<GetProductReviewDTO> GetProductReviewByOrderItemId(int OrderItemId);
        Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId);


        #endregion Ratings & Reviews
    }
}