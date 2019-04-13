using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ProductsDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOptions_ProductAttributes_ProductId_Name",
                table: "AttributeOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOptions_ProductAttributes_ProductId_Name",
                table: "AttributeOptions",
                columns: new[] { "ProductId", "Name" },
                principalTable: "ProductAttributes",
                principalColumns: new[] { "ProductId", "Name" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOptions_ProductAttributes_ProductId_Name",
                table: "AttributeOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOptions_ProductAttributes_ProductId_Name",
                table: "AttributeOptions",
                columns: new[] { "ProductId", "Name" },
                principalTable: "ProductAttributes",
                principalColumns: new[] { "ProductId", "Name" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
