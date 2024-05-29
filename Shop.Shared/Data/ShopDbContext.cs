using Microsoft.EntityFrameworkCore;
using Shop.Shared.Entities;

namespace Shop.Shared.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Generate new Guid values

            var beautyId = 1;
            var furnitureId = 2;
            var electronicsId = 3;
            var shoesId = 4;

            var image1Id = 1;
            var image2Id = 2;

            //Add Product Categories
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = beautyId,
                Name = "Beauty"
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = furnitureId,
                Name = "Furniture"
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = electronicsId,
                Name = "Electronics"
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = shoesId,
                Name = "Shoes"
            });

            modelBuilder.Entity<ProductImage>().HasData(new ProductImage
            {
                Id = 1,
                Name = "Beauty/Beauty1.png",
                ProductId = 1, // assuming this product exists
                StorageContainers = new[] { StorageContainers.shop }
            });
            modelBuilder.Entity<ProductImage>().HasData(new ProductImage
            {
                Id = 2,
                Name = "Beauty/Beauty2.png",
                ProductId = 1, // assuming this product exists
                StorageContainers = new[] { StorageContainers.shop }
            });
            modelBuilder.Entity<ProductImage>().HasData(new ProductImage
            {
                Id = 3,
                Name = "Beauty2.png",
                ProductId = 2, // assuming this product exists
                StorageContainers = new[] { StorageContainers.shop }
            });
            modelBuilder.Entity<ProductImage>().HasData(new ProductImage
            {
                Id = 4,
                Name = "Beauty3.png",
                ProductId = 3, // assuming this product exists
                StorageContainers = new[] { StorageContainers.shop }
            });


            //Products
            //Beauty Category
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Glossier - Beauty Kit",
                Description = "A kit provided by Glossier, containing skin care, hair care and makeup products",
                Price = 100,
                Quantity = 100,
                CategoryId = beautyId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Curology - Skin Care Kit",
                Description = "A kit provided by Curology, containing skin care products",
                Price = 50,
                Quantity = 45,
                CategoryId = beautyId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Cocooil - Organic Coconut Oil",
                Description = "A kit provided by Curology, containing skin care products",
                Price = 20,
                Quantity = 30,
                CategoryId = beautyId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Schwarzkopf - Hair Care and Skin Care Kit",
                Description = "A kit provided by Schwarzkopf, containing skin care and hair care products",
                Price = 50,
                Quantity = 60,
                CategoryId = beautyId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Skin Care Kit",
                Description = "Skin Care Kit, containing skin care and hair care products",
                Price = 30,
                Quantity = 85,
                CategoryId = beautyId

            });
            //Electronics Category
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "Air Pods",
                Description = "Air Pods - in-ear wireless headphones",
                Price = 100,
                Quantity = 120,
                CategoryId = electronicsId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 7,
                Name = "On-ear Golden Headphones",
                Description = "On-ear Golden Headphones - these headphones are not wireless",
                Price = 40,
                Quantity = 200,
                CategoryId = electronicsId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 8,
                Name = "On-ear Black Headphones",
                Description = "On-ear Black Headphones - these headphones are not wireless",
                Price = 40,
                Quantity = 300,
                CategoryId = electronicsId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 9,
                Name = "Sennheiser Digital Camera with Tripod",
                Description = "Sennheiser Digital Camera - High quality digital camera provided by Sennheiser - includes tripod",
                Price = 600,
                Quantity = 20,
                CategoryId = electronicsId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 10,
                Name = "Canon Digital Camera",
                Description = "Canon Digital Camera - High quality digital camera provided by Canon",
                Price = 500,
                Quantity = 15,
                CategoryId = electronicsId

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 11,
                Name = "Nintendo Gameboy",
                Description = "Gameboy - Provided by Nintendo",
                Price = 100,
                Quantity = 60,
                CategoryId = electronicsId
            });
            //Furniture Category
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 12,
                Name = "Black Leather Office Chair",
                Description = "Very comfortable black leather office chair",
                Price = 50,
                Quantity = 212,
                CategoryId = furnitureId
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 13,
                Name = "Pink Leather Office Chair",
                Description = "Very comfortable pink leather office chair",
                Price = 50,
                Quantity = 112,
                CategoryId = furnitureId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 14,
                Name = "Lounge Chair",
                Description = "Very comfortable lounge chair",
                Price = 70,
                Quantity = 90,
                CategoryId = furnitureId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 15,
                Name = "Silver Lounge Chair",
                Description = "Very comfortable Silver lounge chair",
                Price = 120,
                Quantity = 95,
                CategoryId = furnitureId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 16,
                Name = "Porcelain Table Lamp",
                Description = "White and blue Porcelain Table Lamp",
                Price = 15,
                Quantity = 100,
                CategoryId = furnitureId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 17,
                Name = "Office Table Lamp",
                Description = "Office Table Lamp",
                Price = 20,
                Quantity = 73,
                CategoryId = furnitureId
            });
            //Shoes Category
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 18,
                Name = "Puma Sneakers",
                Description = "Comfortable Puma Sneakers in most sizes",
                Price = 100,
                Quantity = 50,
                CategoryId = shoesId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 19,
                Name = "Colorful Trainers",
                Description = "Colorful trainsers - available in most sizes",
                Price = 150,
                Quantity = 60,
                CategoryId = shoesId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 20,
                Name = "Blue Nike Trainers",
                Description = "Blue Nike Trainers - available in most sizes",
                Price = 200,
                Quantity = 70,
                CategoryId = shoesId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 21,
                Name = "Colorful Hummel Trainers",
                Description = "Colorful Hummel Trainers - available in most sizes",
                Price = 120,
                Quantity = 120,
                CategoryId = shoesId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 22,
                Name = "Red Nike Trainers",
                Description = "Red Nike Trainers - available in most sizes",
                Price = 200,
                Quantity = 100,
                CategoryId = shoesId
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 23,
                Name = "Birkenstock Sandles",
                Description = "Birkenstock Sandles - available in most sizes",
                Price = 50,
                Quantity = 150,
                CategoryId = shoesId
            });
        }
    }
}
