using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductCategory> ProductCategorys { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UsersRegister> UsersRegisters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Product_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.Category_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.ProductCategory)
                .HasForeignKey(e => e.Category_id);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.ProductCategory)
                .HasForeignKey(e => e.Category_id);

            modelBuilder.Entity<Product>()
                .Property(e => e.Product_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.Product_id);

            modelBuilder.Entity<UsersRegister>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UsersRegister>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UsersRegister>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<UsersRegister>()
                .Property(e => e.Roll)
                .IsUnicode(false);

            modelBuilder.Entity<UsersRegister>()
                .Property(e => e.Profile_Image)
                .IsUnicode(false);

            modelBuilder.Entity<UsersRegister>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<UsersRegister>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.UsersRegister)
                .HasForeignKey(e => e.User__id);
        }
    }
}
