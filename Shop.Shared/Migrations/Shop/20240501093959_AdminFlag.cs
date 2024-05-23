using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.API.Migrations
{
    /// <inheritdoc />
    public partial class AdminFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Admin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Admin",
                value: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "UserName" },
                values: new object[] { 3, true, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Users");
        }
    }
}
