using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shop.Shared.Migrations.Shop
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentMD5 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StorageContainers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Beauty" },
                    { 2, "Furniture" },
                    { 3, "Electronics" },
                    { 4, "Shoes" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageURL", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "A kit provided by Glossier, containing skin care, hair care and makeup products", null, "Glossier - Beauty Kit", 100m, 100 },
                    { 2, 1, "A kit provided by Curology, containing skin care products", null, "Curology - Skin Care Kit", 50m, 45 },
                    { 3, 1, "A kit provided by Curology, containing skin care products", null, "Cocooil - Organic Coconut Oil", 20m, 30 },
                    { 4, 1, "A kit provided by Schwarzkopf, containing skin care and hair care products", null, "Schwarzkopf - Hair Care and Skin Care Kit", 50m, 60 },
                    { 5, 1, "Skin Care Kit, containing skin care and hair care products", null, "Skin Care Kit", 30m, 85 },
                    { 6, 3, "Air Pods - in-ear wireless headphones", null, "Air Pods", 100m, 120 },
                    { 7, 3, "On-ear Golden Headphones - these headphones are not wireless", null, "On-ear Golden Headphones", 40m, 200 },
                    { 8, 3, "On-ear Black Headphones - these headphones are not wireless", null, "On-ear Black Headphones", 40m, 300 },
                    { 9, 3, "Sennheiser Digital Camera - High quality digital camera provided by Sennheiser - includes tripod", null, "Sennheiser Digital Camera with Tripod", 600m, 20 },
                    { 10, 3, "Canon Digital Camera - High quality digital camera provided by Canon", null, "Canon Digital Camera", 500m, 15 },
                    { 11, 3, "Gameboy - Provided by Nintendo", null, "Nintendo Gameboy", 100m, 60 },
                    { 12, 2, "Very comfortable black leather office chair", null, "Black Leather Office Chair", 50m, 212 },
                    { 13, 2, "Very comfortable pink leather office chair", null, "Pink Leather Office Chair", 50m, 112 },
                    { 14, 2, "Very comfortable lounge chair", null, "Lounge Chair", 70m, 90 },
                    { 15, 2, "Very comfortable Silver lounge chair", null, "Silver Lounge Chair", 120m, 95 },
                    { 16, 2, "White and blue Porcelain Table Lamp", null, "Porcelain Table Lamp", 15m, 100 },
                    { 17, 2, "Office Table Lamp", null, "Office Table Lamp", 20m, 73 },
                    { 18, 4, "Comfortable Puma Sneakers in most sizes", null, "Puma Sneakers", 100m, 50 },
                    { 19, 4, "Colorful trainsers - available in most sizes", null, "Colorful Trainers", 150m, 60 },
                    { 20, 4, "Blue Nike Trainers - available in most sizes", null, "Blue Nike Trainers", 200m, 70 },
                    { 21, 4, "Colorful Hummel Trainers - available in most sizes", null, "Colorful Hummel Trainers", 120m, 120 },
                    { 22, 4, "Red Nike Trainers - available in most sizes", null, "Red Nike Trainers", 200m, 100 },
                    { 23, 4, "Birkenstock Sandles - available in most sizes", null, "Birkenstock Sandles", 50m, 150 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "ContentMD5", "Name", "ProductId", "StorageContainers" },
                values: new object[,]
                {
                    { 1, null, "Image1.png", 1, "[0]" },
                    { 2, null, "Image2.png", 2, "[0]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Name",
                table: "ProductCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
