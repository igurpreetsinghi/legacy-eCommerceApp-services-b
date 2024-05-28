using AutoMapper;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.DTO;
using ECommerceWebAPI.DTO.Category;
using ECommerceWebAPI.DTO.Products;
using ECommerceWebAPI.DTO.Users;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerceWebAPI.Services
{
    public class AdminService : IAdminService
    {
        #region Fields

        private readonly DataContext _context;
        private IMapper _mapper;

        #endregion Fields

        #region Ctor

        public AdminService(DataContext dataContext, IMapper mapper)
        {

            _context = dataContext;
            _mapper = mapper;

        }

        #endregion Ctor

        #region Category

        public async Task<CreateCategoryDTO?> AddCategory(CreateCategoryDTO addCategory)
        {
            try
            {
                if (addCategory == null) return null;

                Category category = _mapper.Map<Category>(addCategory);
                category.IsDeleted = false;
                category.CreatedDate = DateTime.Now;
                await _context.tbl_Category.AddAsync(category);
                await _context.SaveChangesAsync();
                return _mapper.Map<CreateCategoryDTO>(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateCategoryDTO> GetByCategoryId(int categoryId)
        {
            var category = await _context.tbl_Category.Where(_ => _.Id == categoryId).FirstOrDefaultAsync();
            if (category == null) return null;
            var categoryDTO = _mapper.Map<CreateCategoryDTO>(category);
            return categoryDTO;
        }

        public async Task<UpdateCategoryDTO> EditCategory(UpdateCategoryDTO editCategoryData)
        {
            if (editCategoryData != null)
            {
                var editCategory = await _context.tbl_Category.Where(_ => _.Id == editCategoryData.Id).FirstOrDefaultAsync();
                if (editCategory == null) { return null; }

                Category employeedataupdate = _mapper.Map(editCategoryData, editCategory);
                if (employeedataupdate == null) { return null; }
                employeedataupdate.UpdatedDate = DateTime.Now;
                _context.Entry(employeedataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<UpdateCategoryDTO>(employeedataupdate);

            }
            return null;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            bool result = false;
            var category = await _context.tbl_Category.FindAsync(id);

            if (category != null)
            {
                category.UpdatedDate = DateTime.Now;
                category.IsDeleted = true;
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<PagedResponse<Category>> SearchCategory(int pageNumber, int pageSize, string searchKeyword)
        {
            try
            {
                List<Category> categoryList = _context.tbl_Category.Where(x => x.IsDeleted == false).ToList();
                var totalRecords = categoryList.Count;
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    categoryList = categoryList.Where(x => x.Name.Contains(searchKeyword.ToLower())).ToList();
                }
                categoryList = categoryList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var pagedResponse = new PagedResponse<Category>(categoryList, pageNumber, pageSize, totalRecords);
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

        #endregion Category

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

                using (var memoryStream = new MemoryStream())
                {
                    await productData.Pictures.CopyToAsync(memoryStream);
                    Pictures productImage = new Pictures
                    {
                        ProductId = product.Id,
                        ImageData = memoryStream.ToArray(),
                        CreatedDate = DateTime.Now
                    };
                    await _context.tbl_Picture.AddAsync(productImage);
                    await _context.SaveChangesAsync();
                }

                //foreach (var imageFile in productData.Pictures)
                //{
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        await imageFile.CopyToAsync(memoryStream);
                //        Pictures productImage = new Pictures
                //        {
                //            ProductId = product.Id,
                //            ImageData = memoryStream.ToArray(),
                //            CreatedDate = DateTime.Now
                //        };
                //        await _context.tbl_Picture.AddAsync(productImage);
                //        await _context.SaveChangesAsync();
                //    }
                //}

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
            var cat = await this.GetByCategoryId(product.CategoryId);
            productDTO.CategoryName = cat?.Name;
            return productDTO;
        }

        public async Task<UpdateProductDTO> EditProduct(UpdateProductDTO editProductData)
        {
            if (editProductData != null)
            {
                var editProduct = await _context.tbl_Product.Where(_ => _.Id == editProductData.Id).FirstOrDefaultAsync();
                if (editProduct == null) { return null; }

                Product employeedataupdate = _mapper.Map(editProductData, editProduct);
                if (employeedataupdate == null) { return null; }
                employeedataupdate.UpdatedDate = DateTime.Now;
                _context.Entry(employeedataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var editPic = await _context.tbl_Picture.Where(_ => _.ProductId == editProductData.Id).FirstOrDefaultAsync();
                if (editPic == null) { return null; }

                if (editProductData.Pictures != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await editProductData.Pictures.CopyToAsync(memoryStream);

                        editPic.Id = editPic.Id;
                        editPic.ProductId = editPic.ProductId;
                        editPic.ImageData = memoryStream.ToArray();
                        editPic.UpdatedDate = DateTime.Now;

                        _context.Entry(editPic).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }



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


        #endregion Products

        #region  User Management

        public async Task<bool?> AddUser(CreateUserDTO addUser)
        {
            try
            {
                if (addUser == null) return null;

                if (await _context.tbl_User.AnyAsync(u => u.UserName == addUser.UserName || u.Email == addUser.Email))
                {
                    return false; // User already exists
                }

                User user = _mapper.Map<User>(addUser);
                user.Password = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
                user.IsActive = true;
                user.CustomerGuid = Guid.NewGuid();
                user.CreatedDate = DateTime.Now;
                await _context.tbl_User.AddAsync(user);
                await _context.SaveChangesAsync();

                var URole = await _context.tbl_User.Where(_ => _.CustomerGuid == user.CustomerGuid).FirstOrDefaultAsync();
                if (URole == null) { return null; }
                UserRole role = new UserRole();
                role.RoleId = addUser.RoleId;
                role.UserId = URole.Id;
                role.CreatedDate = DateTime.Now;
                await _context.tbl_UserRole.AddAsync(role);
                await _context.SaveChangesAsync();

                var UserDTO = _mapper.Map<CreateUserDTO>(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetUserDTO> GetByUserId(int userId)
        {
            var user = await _context.tbl_User.Where(_ => _.Id == userId).FirstOrDefaultAsync();
            if (user == null) return null;
            var userDto = _mapper.Map<GetUserDTO>(user);
            return userDto;
        }

        public async Task<bool?> EditUser(UpdateUserDTO editUserData)
        {
            if (editUserData != null)
            {
                var editUser = await _context.tbl_User.Where(_ => _.Id == editUserData.Id).FirstOrDefaultAsync();
                if (editUser == null) { return false; }

                User employeedataupdate = _mapper.Map(editUserData, editUser);
                if (employeedataupdate == null) { return false; }
                employeedataupdate.Password = BCrypt.Net.BCrypt.HashPassword(editUserData.Password);
                employeedataupdate.UpdatedDate = DateTime.Now;
                _context.Entry(employeedataupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _mapper.Map<UpdateUserDTO>(employeedataupdate);


                var editRole = await _context.tbl_UserRole.Where(_ => _.UserId == editUserData.Id).FirstOrDefaultAsync();
                if (editRole == null) { return null; }

                editRole.Id = editRole.Id;
                editRole.RoleId = editUserData.RoleId;
                editRole.UserId = editUserData.Id;
                editRole.UpdatedDate = DateTime.Now;

                _context.Entry(editRole).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool?> ChangeUserStatus(int id)
        {
            bool result = false;
            var editUser = await _context.tbl_User.FindAsync(id);

            if (editUser != null)
            {
                editUser.UpdatedDate = DateTime.Now;
                if (editUser.IsActive == false)
                {
                    editUser.IsActive = true;
                }
                else
                {
                    editUser.IsActive = false;
                }
                _context.Entry(editUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<PagedResponse<User>> SearchUsers(int pageNumber, int pageSize, string searchKeyword)
        {
            try
            {
                List<User> userList = _context.tbl_User.Where(x => x.IsActive == true).ToList();
                var totalRecords = userList.Count;
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    userList = userList.Where(x => x.UserName.Contains(searchKeyword)).ToList();
                }
                userList = userList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var pagedResponse = new PagedResponse<User>(userList, pageNumber, pageSize, totalRecords);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetUserDTO> GetUserByEmailId(string emailId)
        {
            var user = await _context.tbl_User.Include(x => x.UserRoles).Where(_ => _.Email == emailId && _.IsActive == true).FirstOrDefaultAsync();
            if (user == null) return null;
            var categoryDTO = _mapper.Map<GetUserDTO>(user);
            return categoryDTO;
        }

        public async Task<List<GetUserRoleDTO>> GetUserRoles()
        {
            try
            {
                var userRoleList = await _context.tbl_Role.Where(x => x.IsDeleted == false).ToListAsync();
                if (userRoleList == null) return null;
                List<GetUserRoleDTO> userRoleDTO = _mapper.Map<List<GetUserRoleDTO>>(userRoleList);
                return userRoleDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion User Management
    }
}