using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeShop.Migrations
{
    public partial class AddProductTypesToProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypesId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypesId",
                table: "Product",
                column: "ProductTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductTypes_ProductTypesId",
                table: "Product",
                column: "ProductTypesId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductTypes_ProductTypesId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypesId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypesId",
                table: "Product");
        }
    }
}
