using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroServiceProject.ProductService.Migrations
{
    /// <inheritdoc />
    public partial class ProductIdMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productId",
                table: "Products",
                newName: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "productId");
        }
    }
}
