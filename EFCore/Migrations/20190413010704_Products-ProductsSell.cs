using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ProductsProductsSell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSell_Products_ProductIdentity",
                table: "ProductSell");

            migrationBuilder.DropIndex(
                name: "IX_ProductSell_ProductIdentity",
                table: "ProductSell");

            migrationBuilder.DropColumn(
                name: "ProductIdentity",
                table: "ProductSell");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "ProductSell",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_ProductId",
                table: "ProductSell",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSell_Products_ProductId",
                table: "ProductSell",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSell_Products_ProductId",
                table: "ProductSell");

            migrationBuilder.DropIndex(
                name: "IX_ProductSell_ProductId",
                table: "ProductSell");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductSell");

            migrationBuilder.AddColumn<string>(
                name: "ProductIdentity",
                table: "ProductSell",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_ProductIdentity",
                table: "ProductSell",
                column: "ProductIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSell_Products_ProductIdentity",
                table: "ProductSell",
                column: "ProductIdentity",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
