using AutoMapper;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StackExchange.Redis;
using System.Data;

namespace ECommerceWebAPI.Services
{
    public class ProductService : IProductService
    {
        #region Fields

        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;

        #endregion Fields

        #region Ctor

        public ProductService(DataContext dataContext, IMapper mapper, IAdminService adminService, IConfiguration configuration)
        {
            _context = dataContext;
            _mapper = mapper;
            _adminService = adminService;
            _configuration = configuration;
        }

        #endregion Ctor

        #region Products
        public async Task<PagedResponse<GetProductDTO>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId)
        {
            try
            {
                List<Product> products = _context.tbl_Product.Include(x => x.Category).Where(x => x.IsDeleted == false).ToList();
                var totalRecords = products.Count;
                products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                if (categoryId > 0)
                {
                    products = products.Where(x => x.CategoryId == categoryId && x.IsDeleted == false).ToList();
                }
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    products = products.Where(x => x.Name.ToUpper().Contains(searchKeyword.ToUpper()) || x.Description.ToUpper().Contains(searchKeyword.ToUpper()) || x.CompanyName.ToUpper().Contains(searchKeyword.ToUpper())).ToList();
                }
                //products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var productDTO = _mapper.Map<IEnumerable<GetProductDTO>>(products).ToList();

                var pagedResponse = new PagedResponse<GetProductDTO>(productDTO, pageNumber, pageSize, totalRecords);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object?> SearchProductList()
        {
            try
            {
                List<Product> products = _context.tbl_Product.Include(x => x.Category).Where(x => x.IsDeleted == false).ToList();
                if (products == null) return null;

                var productDTO = _mapper.Map<IEnumerable<GetProductDTO>>(products).ToList();


                //var result = new
                //{
                //    TotalCount = totalCount,
                //    TotalPages = totalPages,
                //    CurrentPage = pageNumber,
                //    PageSize = pageSize,
                //    Products = productDTO
                //};
                return productDTO;

                //products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                //var productDTO = _mapper.Map<IEnumerable<GetProductDTO>>(products).ToList();

                //var pagedResponse = new PagedResponse<GetProductDTO>(productDTO, pageNumber, pageSize, totalRecords);
                //return productDTO;
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

        public async Task<CheckIsProductAvailableInWishlistDTO?> CheckIsProductAvailableInWishlist(int UserId, int productId)
        {

            var Wishlist = await _context.tbl_ProductWishlist.Where(_ => _.UserId == UserId && _.ProductId == productId).FirstOrDefaultAsync();

            if (Wishlist == null) return null;

            var WishlistDTO = _mapper.Map<CheckIsProductAvailableInWishlistDTO>(Wishlist);
            return WishlistDTO;
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

        #region Shopping Cart
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

        public async Task<bool?> DeleteItemFromYourCart(int id)
        {
            bool result = false;
            var editCart = await _context.tbl_ShoppingCart.FindAsync(id);

            if (editCart != null)
            {
                _context.Entry(editCart).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<bool?> ClearCart(int UserId)
        {
            bool result = false;

            var editCart = await _context.tbl_ShoppingCart.Where(x => x.UserId == UserId).ToListAsync();
            if (editCart == null) return null;

            foreach (var item in editCart)
            {
                _context.Entry(item).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        #endregion Shopping Cart

        #region Order
        public async Task<bool?> PlaceOrder(AddPlaceOrderDTO productOrderData)
        {
            try
            {
                if (productOrderData == null) return null;

                if (!await _context.tbl_ShoppingCart.AnyAsync(u => u.UserId == productOrderData.UserId))
                {
                    return false;
                }
                else
                {
                    Orders order = _mapper.Map<Orders>(productOrderData);
                    order.OrderGuid = Guid.NewGuid();
                    order.CreatedDate = DateTime.Now;
                    await _context.tbl_Orders.AddAsync(order);
                    await _context.SaveChangesAsync();

                    if (order.Id != 0)
                    {
                        string connStr = _configuration.GetConnectionString("PgSQLAppCon");
                        //var PGconn = new NpgsqlConnection(connStr);
                        //PGconn.Open();

                        var ShoppingCart = await _context.tbl_ShoppingCart.Where(x => x.UserId == order.UserId).ToListAsync();
                        if (ShoppingCart == null) return null;

                        if (ShoppingCart != null)
                        {
                            foreach (var OrdrItem in ShoppingCart)
                            {
                                var item = new OrderItem();

                                item.ProductId = OrdrItem.ProductId;
                                item.Quantity = OrdrItem.Quantity;
                                item.OrderId = order.Id;
                                item.CreatedDate = DateTime.Now;
                                await _context.tbl_OrderItem.AddAsync(item);
                                await _context.SaveChangesAsync();

                                var Product = _context.tbl_Product.FirstOrDefault(pd => pd.Id == OrdrItem.ProductId);
                                if (Product == null) return null;
                                var ProductDTO = _mapper.Map<GetProductDTO>(Product);

                                //NpgsqlCommand cmd = new NpgsqlCommand();
                                //cmd.Connection = PGconn;
                                //cmd.CommandText = "Insert into public.order_details values(@OrderTotal,@Quantity,@OrderId,@ItemPrice,@ItemName,@OrderDate)";
                                //cmd.CommandType = CommandType.Text;
                                //cmd.Parameters.Add(new NpgsqlParameter("@OrderTotal", Convert.ToDecimal(productOrderData.OrderTotal)));
                                //cmd.Parameters.Add(new NpgsqlParameter("@Quantity", OrdrItem.Quantity));
                                //cmd.Parameters.Add(new NpgsqlParameter("@OrderId", order.OrderGuid));
                                //cmd.Parameters.Add(new NpgsqlParameter("@ItemPrice", ProductDTO.Price));
                                //cmd.Parameters.Add(new NpgsqlParameter("@ItemName", ProductDTO.Name));
                                //cmd.Parameters.Add(new NpgsqlParameter("@OrderDate", DateTime.Now));
                                //cmd.ExecuteNonQuery();
                                //cmd.Dispose();

                                using (var connection = new NpgsqlConnection(connStr))
                                {
                                    if(connection.State != ConnectionState.Closed)
                                    { 
                                    // Open the connection asynchronously
                                    await connection.OpenAsync();

                                    // Check the connection state
                                    var connectionState = connection.State;

                                        // Do something based on the connection state
                                        if (connectionState == ConnectionState.Open)
                                        {
                                            // Connection is open

                                            NpgsqlCommand cmd = new NpgsqlCommand();
                                            cmd.Connection = connection;
                                            cmd.CommandText = "Insert into public.order_details values(@OrderTotal,@Quantity,@OrderId,@ItemPrice,@ItemName,@OrderDate)";
                                            cmd.CommandType = CommandType.Text;
                                            cmd.Parameters.Add(new NpgsqlParameter("@OrderTotal", Convert.ToDecimal(productOrderData.OrderTotal)));
                                            cmd.Parameters.Add(new NpgsqlParameter("@Quantity", OrdrItem.Quantity));
                                            cmd.Parameters.Add(new NpgsqlParameter("@OrderId", order.OrderGuid));
                                            cmd.Parameters.Add(new NpgsqlParameter("@ItemPrice", ProductDTO.Price));
                                            cmd.Parameters.Add(new NpgsqlParameter("@ItemName", ProductDTO.Name));
                                            cmd.Parameters.Add(new NpgsqlParameter("@OrderDate", DateTime.Now));
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                        }

                                    }
                                }

                                var editCart = await _context.tbl_ShoppingCart.FindAsync(OrdrItem.Id);
                                if (editCart != null)
                                {
                                    _context.Entry(editCart).State = EntityState.Deleted;
                                    await _context.SaveChangesAsync();
                                }

                            }
                        }



                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetYourOrderDTO>> GetYourOrder(int UserId)
        {
            try
            {

                List<GetYourOrderDTO> resultList = new List<GetYourOrderDTO>();

                var query = await _context.tbl_Orders.Where(x => x.UserId == UserId).ToListAsync();
                if (query == null) return null;

                foreach (var result in query)
                {

                    var orderItems = await _context.tbl_OrderItem.Include(x => x.Product).Where(pd => pd.OrderId == result.Id).ToListAsync();

                    foreach (var item in orderItems)
                    {
                        GetYourOrderDTO dto = new GetYourOrderDTO
                        {
                            OrderId = result.Id,
                            UserId = UserId,
                            ProductId = item.ProductId,
                            OrderRefNo = result.OrderGuid,
                            OrderTotal = result.OrderTotal,
                            OrderDate = result.CreatedDate,
                            Name = item.Product.Name,
                            Price = item.Product.Price,
                            CompanyName = item.Product.CompanyName,
                            Description = item.Product.Description,
                            ImageData = item.Product.ImageData,
                            OrderItemId = item.Id

                        };
                        resultList.Add(dto);

                    }
                }
                return resultList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion Order

        #region Ratings & Reviews
        public async Task<bool?> AddRatingReviews(AddProductReviewDTO addProductReview)
        {
            try
            {
                if (addProductReview == null) return null;

                if (await _context.tbl_ProductReview.AnyAsync(u => u.OrderItemId == addProductReview.OrderItemId))
                {
                    return false; // Product Review already exists
                }
                ProductReview sReview = _mapper.Map<ProductReview>(addProductReview);
                if (addProductReview.Pictures != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await addProductReview.Pictures.CopyToAsync(memoryStream);

                        sReview.ImageData = memoryStream.ToArray();
                    };
                }
                sReview.CreatedDate = DateTime.Now;
                await _context.tbl_ProductReview.AddAsync(sReview);
                await _context.SaveChangesAsync();
                var sReviewDTO = _mapper.Map<AddProductReviewDTO>(sReview);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool?> EditRatingReviews(UpdateProductReviewDTO editProductReviewData)
        {
            if (editProductReviewData != null)
            {
                var editProductReview = await _context.tbl_ProductReview.Where(_ => _.Id == editProductReviewData.Id).FirstOrDefaultAsync();
                if (editProductReview == null) { return null; }

                ProductReview Reviewdataupdate = _mapper.Map(editProductReviewData, editProductReview);
                if (Reviewdataupdate == null) { return null; }
                if (editProductReviewData.Pictures != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await editProductReviewData.Pictures.CopyToAsync(memoryStream);

                        Reviewdataupdate.ImageData = memoryStream.ToArray();
                    };
                }
                Reviewdataupdate.UpdatedDate = DateTime.Now;
                _context.Entry(Reviewdataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var sReviewDTO = _mapper.Map<UpdateProductReviewDTO>(Reviewdataupdate);

            }
            return true;
        }

        public async Task<GetProductReviewDTO> GetProductReviewByOrderItemId(int OrderItemId)
        {
            var ProductReview = await _context.tbl_ProductReview.Where(_ => _.OrderItemId == OrderItemId).FirstOrDefaultAsync();

            if (ProductReview == null) return null;

            var productDTO = _mapper.Map<GetProductReviewDTO>(ProductReview);
            return productDTO;
        }

        public async Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId)
        {
            try
            {
                var ProductReview = await _context.tbl_ProductReview.Where(x => x.ProductId == productId).ToListAsync();
                if (ProductReview == null) return null;

                var ProductReviewDTOs = ProductReview.Select(pw =>
                {
                    var Userdetails = _context.tbl_User.FirstOrDefault(pd => pd.Id == pw.UserId);
                    var dto = _mapper.Map<GetProductReviewDTO>(pw);
                    if (Userdetails != null)
                    {
                        dto.Description = pw.Description;
                        dto.Title = pw.Title;
                        dto.Rating = pw.Rating;
                        dto.UserName = Userdetails.UserName;
                        dto.ImageData = pw.ImageData;
                        dto.ReviewedDate = pw.CreatedDate;
                    }
                    return dto;
                }).ToList();

                return ProductReviewDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion Ratings & Reviews


    }
}