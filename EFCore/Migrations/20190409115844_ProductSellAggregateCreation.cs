using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ProductSellAggregateCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Products",
                type: "decimal(8,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.CreateTable(
                name: "ProductSell",
                columns: table => new
                {
                    ProductSellId = table.Column<string>(nullable: false),
                    IsReleasable = table.Column<bool>(nullable: false),
                    IsReleased = table.Column<bool>(nullable: false),
                    ProductIdentity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSell", x => x.ProductSellId);
                    table.ForeignKey(
                        name: "FK_ProductSell_Products_ProductIdentity",
                        column: x => x.ProductIdentity,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombinations",
                columns: table => new
                {
                    SignupCount = table.Column<long>(nullable: false),
                    ProductPrice_Discount = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ProductPrice_Price = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ProductPrice_LowestPrice = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ProductSellId = table.Column<string>(nullable: false),
                    ProductCombinationId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombinations", x => new { x.ProductSellId, x.ProductCombinationId });
                    table.ForeignKey(
                        name: "FK_ProductCombinations_ProductSell_ProductSellId",
                        column: x => x.ProductSellId,
                        principalTable: "ProductSell",
                        principalColumn: "ProductSellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellSignups",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProductSellId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellSignups", x => x.ProductSellId);
                    table.ForeignKey(
                        name: "FK_SellSignups_ProductSell_ProductSellId",
                        column: x => x.ProductSellId,
                        principalTable: "ProductSell",
                        principalColumn: "ProductSellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelectedAttributes",
                columns: table => new
                {
                    AttributeName = table.Column<string>(nullable: false),
                    SelectedOption = table.Column<string>(nullable: false),
                    ProductSellId = table.Column<string>(nullable: false),
                    ProductCombinationId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedAttributes", x => new { x.ProductSellId, x.ProductCombinationId });
                    table.ForeignKey(
                        name: "FK_SelectedAttributes_ProductCombinations_ProductSellId_ProductCombinationId",
                        columns: x => new { x.ProductSellId, x.ProductCombinationId },
                        principalTable: "ProductCombinations",
                        principalColumns: new[] { "ProductSellId", "ProductCombinationId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_ProductIdentity",
                table: "ProductSell",
                column: "ProductIdentity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedAttributes");

            migrationBuilder.DropTable(
                name: "SellSignups");

            migrationBuilder.DropTable(
                name: "ProductCombinations");

            migrationBuilder.DropTable(
                name: "ProductSell");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Products",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,4)");
        }
    }
}
