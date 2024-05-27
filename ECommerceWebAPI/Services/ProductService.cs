using AutoMapper;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
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
        public async Task<PagedResponse<Product>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId)
        {
            try
            {
                List<Product> products = _context.tbl_Product.Where(x => x.IsDeleted == false).ToList();
                var totalRecords = products.Count;
                if (categoryId > 0)
                {
                    products = products.Where(x => x.CategoryId == categoryId).ToList();
                }
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    products = products.Where(x => x.Name.Contains(searchKeyword)).ToList();
                }
                products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var pagedResponse = new PagedResponse<Product>(products, pageNumber, pageSize, totalRecords);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<CreateCategoryDTO>> GetAllCategory()
        {
            var category = await _context.tbl_Category.ToListAsync();
            if (category == null) return null;
            List<CreateCategoryDTO> categoryDTO = _mapper.Map<List<CreateCategoryDTO>>(category);
            return categoryDTO;
        }

        public async Task<CreateCategoryDTO> GetByCategoryId(int categoryId)
        {
            var category = await _context.tbl_Category.Where(_ => _.Id == categoryId).FirstOrDefaultAsync();
            if (category == null) return null;
            var categoryDTO = _mapper.Map<CreateCategoryDTO>(category);
            return categoryDTO;
        }
        public async Task<GetProductDTO> GetByProductId(int productId)
        {
            var product = await _context.tbl_Product.Where(_ => _.Id == productId).FirstOrDefaultAsync();

            if (product == null) return null;

            var productDTO = _mapper.Map<GetProductDTO>(product);
            var pictures = _context.tbl_Picture.Where(_ => _.ProductId == product.Id).ToList();
            productDTO.Pictures = _mapper.Map<List<PictureDTO>>(pictures);
            var cat = await this.GetByCategoryId(product.CategoryId);
            productDTO.CategoryName = cat?.Name;
            return productDTO;
        }

        #region Product Reviews
        public async Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId)
        {
            var product = await _context.tbl_ProductReview.Where(_ => _.Id == productId).FirstOrDefaultAsync();

            if (product == null) return null;

            List<GetProductReviewDTO> productReviewDTOs = _mapper.Map<List<GetProductReviewDTO>>(product);

            return productReviewDTOs;
        }

        #endregion Product Reviews
        public async Task<List<GetProductReviewDTO>> AddProductToWishlist(int productId)
        {
            var product = await _context.tbl_ProductReview.Where(_ => _.Id == productId).FirstOrDefaultAsync();

            if (product == null) return null;

            List<GetProductReviewDTO> productReviewDTOs = _mapper.Map<List<GetProductReviewDTO>>(product);

            return productReviewDTOs;
        }
        
        #endregion Products

    }
}