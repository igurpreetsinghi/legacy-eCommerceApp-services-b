using ECommerceWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebAPI.Context
{
    public class DataContext
        : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> tbl_User => Set<User>();
        public DbSet<Category> tbl_Category => Set<Category>();
        public DbSet<Orders> tbl_Orders => Set<Orders>();
        public DbSet<OrderItem> tbl_OrderItem => Set<OrderItem>();
        public DbSet<Product> tbl_Product => Set<Product>();
        public DbSet<ProductReview> tbl_ProductReview => Set<ProductReview>();
        public DbSet<ShoppingCart> tbl_ShoppingCart => Set<ShoppingCart>();
        public DbSet<Role> tbl_Role => Set<Role>();
        public DbSet<Address> tbl_Address => Set<Address>();

        public DbSet<ProductWishlist> tbl_ProductWishlist => Set<ProductWishlist>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<OrderItem>().HasOne(_ => _.Order).WithMany(_ => _.OrderItems).HasForeignKey(_ => _.OrderId);
            modelBuilder.Entity<OrderItem>().HasOne(_ => _.User).WithMany(_ => _.OrderItems).HasForeignKey(_ => _.UserId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<OrderItem>().HasOne(_ => _.Product).WithMany(_ => _.OrderItems).HasForeignKey(_ => _.ProductId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<OrderItem>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Product>().HasOne(_ => _.Category).WithMany(_ => _.Product).HasForeignKey(_ => _.CategoryId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Product>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Orders>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ShoppingCart>().HasOne(_ => _.User).WithMany(_ => _.ShoppingCart).HasForeignKey(_ => _.UserId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ShoppingCart>().HasOne(_ => _.Product).WithMany(_ => _.ShoppingCart).HasForeignKey(_ => _.ProductId).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ProductReview>().HasOne(_ => _.Product).WithMany(_ => _.ProductReview).HasForeignKey(_ => _.ProductId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ProductReview>()
               .Property(b => b.CreatedDate)
               .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ProductWishlist>().HasOne(_ => _.Product).WithMany(_ => _.ProductWishlist).HasForeignKey(_ => _.ProductId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ProductWishlist>()
               .Property(b => b.CreatedDate)
               .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Address>().HasOne(_ => _.User).WithMany(_ => _.Address).HasForeignKey(_ => _.UserId).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Address>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

        }
    }
}
