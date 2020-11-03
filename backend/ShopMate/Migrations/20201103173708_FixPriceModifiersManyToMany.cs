using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class FixPriceModifiersManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceModifier_Products_ProductBarcode",
                table: "PriceModifier");

            migrationBuilder.DropIndex(
                name: "IX_PriceModifier_ProductBarcode",
                table: "PriceModifier");

            migrationBuilder.DropColumn(
                name: "ProductBarcode",
                table: "PriceModifier");

            migrationBuilder.CreateTable(
                name: "PriceModifierProduct",
                columns: table => new
                {
                    PriceModifiersId = table.Column<int>(type: "int", nullable: false),
                    ProductsBarcode = table.Column<string>(type: "char(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceModifierProduct", x => new { x.PriceModifiersId, x.ProductsBarcode });
                    table.ForeignKey(
                        name: "FK_PriceModifierProduct_PriceModifier_PriceModifiersId",
                        column: x => x.PriceModifiersId,
                        principalTable: "PriceModifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceModifierProduct_Products_ProductsBarcode",
                        column: x => x.ProductsBarcode,
                        principalTable: "Products",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifierProduct_ProductsBarcode",
                table: "PriceModifierProduct",
                column: "ProductsBarcode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceModifierProduct");

            migrationBuilder.AddColumn<string>(
                name: "ProductBarcode",
                table: "PriceModifier",
                type: "char(14)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifier_ProductBarcode",
                table: "PriceModifier",
                column: "ProductBarcode");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceModifier_Products_ProductBarcode",
                table: "PriceModifier",
                column: "ProductBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
