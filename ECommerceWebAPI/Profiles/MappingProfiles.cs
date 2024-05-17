using AutoMapper;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Models;

namespace ECommerceWebAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();



            CreateMap<Pictures, PictureDTO>().ReverseMap();


            CreateMap<CreateUserDTO, User>().ReverseMap();
            CreateMap<UpdateUserDTO, User>().ReverseMap();
            CreateMap<User, GetUserDTO>().ReverseMap();
            CreateMap<Role, GetUserRoleDTO>().ReverseMap();




            CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDTO<>));


        }
    }
}
