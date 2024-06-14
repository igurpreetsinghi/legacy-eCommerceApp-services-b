using AutoMapper;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
            CreateMap<GetProductDTO, Product>().ReverseMap().ForMember(dest => dest.CategoryName, x => x.MapFrom(src => src.Category.Name));

            CreateMap<GetUserRoleDTO, Role>().ReverseMap();
            CreateMap<CreateUserDTO, User>().ReverseMap();
            CreateMap<UpdateUserDTO, User>().ReverseMap();
            CreateMap<GetUserDTO, User>().ReverseMap();

            CreateMap<AddProductToWishlistDTO, ProductWishlist>().ReverseMap();
            CreateMap<YourWishlistDTO, ProductWishlist>().ReverseMap();

            CreateMap<AddShippingAddressDTO, Address>().ReverseMap();
            CreateMap<GetShippingAddressDTO, Address>().ReverseMap();
            CreateMap<UpdateShippingAddressDTO, Address>().ReverseMap();

            CreateMap<AddProductToCartDTO, ShoppingCart>().ReverseMap();
            CreateMap<GetYourCartDTO, ShoppingCart>().ReverseMap();
            CreateMap<UpdateCartItemQuantityDTO, ShoppingCart>().ReverseMap();

            CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDTO<>));


        }
    }
}
