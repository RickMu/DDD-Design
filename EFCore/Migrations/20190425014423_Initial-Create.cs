using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    AttributeType = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => new { x.ProductId, x.Name });
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSell",
                columns: table => new
                {
                    ProductSellId = table.Column<string>(nullable: false),
                    IsReleasable = table.Column<bool>(nullable: false),
                    IsReleased = table.Column<bool>(nullable: false),
                    ProductId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSell", x => x.ProductSellId);
                    table.ForeignKey(
                        name: "FK_ProductSell_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttributeOptions",
                columns: table => new
                {
                    Value = table.Column<string>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOptions", x => new { x.ProductId, x.Name, x.Value });
                    table.ForeignKey(
                        name: "FK_AttributeOptions_ProductAttributes_ProductId_Name",
                        columns: x => new { x.ProductId, x.Name },
                        principalTable: "ProductAttributes",
                        principalColumns: new[] { "ProductId", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombinations",
                columns: table => new
                {
                    ProductCombinationId = table.Column<string>(nullable: false),
                    ProductSellId = table.Column<string>(nullable: false),
                    SignupCount = table.Column<long>(nullable: false),
                    ProductPrice_Discount = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ProductPrice_Price = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ProductPrice_LowestPrice = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
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
                    ProductSellId = table.Column<string>(nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
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
                    ProductSellId = table.Column<string>(nullable: false),
                    ProductCombinationId = table.Column<string>(nullable: false),
                    SelectedOption = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedAttributes", x => new { x.ProductSellId, x.ProductCombinationId, x.AttributeName });
                    table.ForeignKey(
                        name: "FK_SelectedAttributes_ProductCombinations_ProductSellId_ProductCombinationId",
                        columns: x => new { x.ProductSellId, x.ProductCombinationId },
                        principalTable: "ProductCombinations",
                        principalColumns: new[] { "ProductSellId", "ProductCombinationId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_ProductId",
                table: "ProductSell",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeOptions");

            migrationBuilder.DropTable(
                name: "SelectedAttributes");

            migrationBuilder.DropTable(
                name: "SellSignups");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "ProductCombinations");

            migrationBuilder.DropTable(
                name: "ProductSell");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
