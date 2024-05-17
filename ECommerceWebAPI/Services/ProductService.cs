using AutoMapper;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.DTO;
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

        public async Task<CreateProductDTO?> AddProduct(CreateProductDTO productData)
        {
            try
            {
                if (productData == null) return null;

                Product product = new Product();
                product.Name = productData.Name;
                product.Description = productData.Description;
                product.CategoryId = productData.CategoryId;
                product.Price = productData.Price;
                product.CompanyName = productData.CompanyName;
                product.IsDeleted = false;
                product.CreatedDate = DateTime.Now;
                await _context.tbl_Product.AddAsync(product);
                await _context.SaveChangesAsync();

                foreach (var imageFile in productData.Pictures)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        Pictures productImage = new Pictures
                        {
                            ProductId = product.Id,
                            ImageData = memoryStream.ToArray()
                        };
                        await _context.tbl_Picture.AddAsync(productImage);
                        await _context.SaveChangesAsync();
                    }
                }

                return productData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetProductDTO> GetByProductId(int productId)
        {
            var product = await _context.tbl_Product.Where(_ => _.Id == productId).FirstOrDefaultAsync();

            if (product == null) return null;

            var productDTO = _mapper.Map<GetProductDTO>(product);
            var pictures = _context.tbl_Picture.Where(_ => _.ProductId == product.Id).ToList();
            productDTO.Pictures = _mapper.Map<List<PictureDTO>>(pictures);
            var cat = await _adminService.GetByCategoryId(product.CategoryId);
            productDTO.CategoryName = cat?.Name;
            return productDTO;
        }

        public async Task<UpdateProductDTO> EditProduct(UpdateProductDTO editCategoryData)
        {
            if (editCategoryData != null)
            {
                var editCategory = await _context.tbl_Product.Where(_ => _.Id == editCategoryData.Id).FirstOrDefaultAsync();
                if (editCategory == null) { return null; }

                Product employeedataupdate = _mapper.Map(editCategoryData, editCategory);
                if (employeedataupdate == null) { return null; }
                _context.Entry(employeedataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<UpdateProductDTO>(employeedataupdate);

            }
            return null;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            bool result = false;
            var product = await _context.tbl_Product.FindAsync(id);

            if (product != null)
            {
                product.UpdatedDate = DateTime.Now;
                product.IsDeleted = true;
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<PagedResponse<Product>> SearchProducts(int pageNumber, int pageSize, string searchKeyword, int categoryId)
        {
            try
            {
                List<Product> products = _context.tbl_Product.Where(x => x.IsDeleted == false).ToList();
                var totalRecords = products.Count;
                if (categoryId < 0)
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

        public async Task<List<GetProductReviewDTO>> GetProductReviewByProductId(int productId)
        {
            var product = await _context.tbl_ProductReview.Where(_ => _.Id == productId).FirstOrDefaultAsync();

            if (product == null) return null;

            List<GetProductReviewDTO> productReviewDTOs = _mapper.Map<List<GetProductReviewDTO>>(product);

            return productReviewDTOs;
        }

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