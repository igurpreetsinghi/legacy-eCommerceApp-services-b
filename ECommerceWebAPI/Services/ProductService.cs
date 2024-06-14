using AutoMapper;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebAPI.Services
{
    public class ProductService : IProductService
    {
        #region Fields

        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly IAdminService _adminService;

        #endregion Fields

        #region Ctor

        public ProductService(DataContext dataContext, IMapper mapper, IAdminService adminService)
        {
            _context = dataContext;
            _mapper = mapper;
            _adminService = adminService;
        }

        #endregion Ctor

        #region Products
        public async Task<PagedResponse<GetProductDTO>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId)
        {
            try
            {
                List<Product> products = _context.tbl_Product.Include(x => x.Category).Where(x => x.IsDeleted == false).ToList();
                var totalRecords = products.Count;
                if (categoryId > 0)
                {
                    products = products.Where(x => x.CategoryId == categoryId && x.IsDeleted == false).ToList();
                }
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    products = products.Where(x => x.Name.Contains(searchKeyword)).ToList();
                }
                products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var productDTO = _mapper.Map<IEnumerable<GetProductDTO>>(products).ToList();

                var pagedResponse = new PagedResponse<GetProductDTO>(productDTO, pageNumber, pageSize, totalRecords);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<UpdateCategoryDTO>> GetAllCategory()
        {
            var category = await _context.tbl_Category.Where(_ => _.IsDeleted == false).ToListAsync();
            if (category == null) return null;
            List<UpdateCategoryDTO> categoryDTO = _mapper.Map<List<UpdateCategoryDTO>>(category);
            return categoryDTO;
        }

        public async Task<CreateCategoryDTO> GetByCategoryId(int categoryId)
        {
            var category = await _context.tbl_Category.Where(_ => _.Id == categoryId && _.IsDeleted == false).FirstOrDefaultAsync();
            if (category == null) return null;
            var categoryDTO = _mapper.Map<CreateCategoryDTO>(category);
            return categoryDTO;
        }
        public async Task<GetProductDTO> GetByProductId(int productId)
        {
            var product = await _context.tbl_Product.Where(_ => _.Id == productId && _.IsDeleted == false).FirstOrDefaultAsync();

            if (product == null) return null;

            var productDTO = _mapper.Map<GetProductDTO>(product);
            var cat = await this.GetByCategoryId(product.CategoryId);
            productDTO.CategoryName = cat?.Name;
            return productDTO;
        }

        #endregion Product 
        public async Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId)
        {
            var product = await _context.tbl_ProductReview.Where(_ => _.Id == productId).FirstOrDefaultAsync();

            if (product == null) return null;

            List<GetProductReviewDTO> productReviewDTOs = _mapper.Map<List<GetProductReviewDTO>>(product);

            return productReviewDTOs;
        }

        #region Product Wishlist
        public async Task<AddProductToWishlistDTO?> AddProductToWishlist(AddProductToWishlistDTO addProductWishlist)
        {
            try
            {
                if (addProductWishlist == null) return null;

                ProductWishlist Wishlist = _mapper.Map<ProductWishlist>(addProductWishlist);
                Wishlist.InWishlist = true;
                Wishlist.CreatedDate = DateTime.Now;
                await _context.tbl_ProductWishlist.AddAsync(Wishlist);
                await _context.SaveChangesAsync();
                return _mapper.Map<AddProductToWishlistDTO>(Wishlist);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<YourWishlistDTO>> YourWishlist(int UserId)
        {
            try
            {
                var ProductWishlist = await _context.tbl_ProductWishlist.Where(x => x.InWishlist == true && x.UserId == UserId).ToListAsync();
                if (ProductWishlist == null) return null;

                var wishlistDTOs = ProductWishlist.Select(pw =>
                {
                    var details = _context.tbl_Product.FirstOrDefault(pd => pd.Id == pw.ProductId);
                    var dto = _mapper.Map<YourWishlistDTO>(pw);
                    if (details != null)
                    {
                        dto.Id = details.Id;
                        dto.WishlistId = pw.Id;
                        dto.Name = details.Name;
                        dto.CompanyName = details.CompanyName;
                        dto.Price = details.Price;
                        dto.ImageData = details.ImageData;
                    }
                    return dto;
                }).ToList();

                return wishlistDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool?> RemoveProductWishlist(int id)
        {
            bool result = false;
            var editWishlist = await _context.tbl_ProductWishlist.FindAsync(id);

            if (editWishlist != null)
            {
                editWishlist.UpdatedDate = DateTime.Now;
                if (editWishlist.InWishlist == true)
                {
                    editWishlist.InWishlist = false;
                }
                _context.Entry(editWishlist).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        #endregion Products Wishlist

        #region Shipping Address
        public async Task<bool?> AddShippingAddress(AddShippingAddressDTO addShippingAddress)
        {
            try
            {
                if (addShippingAddress == null) return null;

                if (await _context.tbl_Address.AnyAsync(u => u.UserId == addShippingAddress.UserId))
                {
                    return false; // Address already exists
                }

                Address address = _mapper.Map<Address>(addShippingAddress);
                address.CreatedDate = DateTime.Now;
                await _context.tbl_Address.AddAsync(address);
                await _context.SaveChangesAsync();
                var AddressDTO = _mapper.Map<AddShippingAddressDTO>(address);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetShippingAddressDTO> GetShippingAddressByUserId(int UserId)
        {
            var Address = await _context.tbl_Address.Where(_ => _.UserId == UserId).FirstOrDefaultAsync();

            if (Address == null) return null;

            var AddressDTO = _mapper.Map<GetShippingAddressDTO>(Address);
            return AddressDTO;
        }

        public async Task<bool?> EditShippingAddress(UpdateShippingAddressDTO updateAddressDTO)
        {
            if (updateAddressDTO != null)
            {
                var editAddress = await _context.tbl_Address.Where(_ => _.Id == updateAddressDTO.Id).FirstOrDefaultAsync();
                if (editAddress == null) { return false; }

                Address Adddataupdate = _mapper.Map(updateAddressDTO, editAddress);
                if (Adddataupdate == null) { return false; }
                Adddataupdate.UpdatedDate = DateTime.Now;
                _context.Entry(Adddataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _mapper.Map<UpdateShippingAddressDTO>(Adddataupdate);

                return true;
            }
            return false;
        }


        #endregion Shipping Address

        #region Cart
        public async Task<bool?> AddProductToCart(AddProductToCartDTO addToCart)
        {
            try
            {
                if (addToCart == null) return null;

                if (await _context.tbl_ShoppingCart.AnyAsync(u => u.ProductId == addToCart.ProductId && u.UserId == addToCart.UserId))
                {
                    return false; // Product already exists
                }

                ShoppingCart sCart = _mapper.Map<ShoppingCart>(addToCart);
                sCart.CreatedDate = DateTime.Now;
                await _context.tbl_ShoppingCart.AddAsync(sCart);
                await _context.SaveChangesAsync();
                var sCartDTO = _mapper.Map<AddProductToCartDTO>(sCart);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetYourCartDTO>> GetCartByUserId(int UserId)
        {
            try
            {
                var ShoppingCart = await _context.tbl_ShoppingCart.Where(x => x.UserId == UserId).ToListAsync();
                if (ShoppingCart == null) return null;

                var ShoppingCartDTOs = ShoppingCart.Select(pw =>
                {
                    var details = _context.tbl_Product.FirstOrDefault(pd => pd.Id == pw.ProductId);
                    var dto = _mapper.Map<GetYourCartDTO>(pw);
                    if (details != null)
                    {
                        dto.Id = pw.Id;
                        dto.ProductId = details.Id;
                        dto.Name = details.Name;
                        dto.CompanyName = details.CompanyName;
                        dto.Description = details.Description;
                        dto.Price = details.Price;
                        dto.ImageData = details.ImageData;
                        dto.Quantity = pw.Quantity;
                        dto.UserId = pw.UserId;
                    }
                    return dto;
                }).ToList();

                return ShoppingCartDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool?> EditCartItemQuantity(UpdateCartItemQuantityDTO editCartQuantityDTOs)
        {
            if (editCartQuantityDTOs != null)
            {
                var editCartQuantity = await _context.tbl_ShoppingCart.Where(_ => _.Id == editCartQuantityDTOs.Id).FirstOrDefaultAsync();
                if (editCartQuantity == null) { return false; }

                ShoppingCart Cartdataupdate = _mapper.Map(editCartQuantityDTOs, editCartQuantity);
                if (Cartdataupdate == null) { return false; }
                Cartdataupdate.UpdatedDate = DateTime.Now;
                _context.Entry(Cartdataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _mapper.Map<UpdateCartItemQuantityDTO>(Cartdataupdate);

                return true;
            }
            return false;
        }


        #endregion Cart

    }
}