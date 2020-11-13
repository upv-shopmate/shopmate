using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class Añadida_relacion_muchos_a_muchos_de_cupones_productos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Coupons_CouponCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CouponCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "CouponProduct",
                columns: table => new
                {
                    AffectedByCouponsCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicableProductsBarcode = table.Column<string>(type: "char(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponProduct", x => new { x.AffectedByCouponsCode, x.ApplicableProductsBarcode });
                    table.ForeignKey(
                        name: "FK_CouponProduct_Coupons_AffectedByCouponsCode",
                        column: x => x.AffectedByCouponsCode,
                        principalTable: "Coupons",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponProduct_Products_ApplicableProductsBarcode",
                        column: x => x.ApplicableProductsBarcode,
                        principalTable: "Products",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponProduct_ApplicableProductsBarcode",
                table: "CouponProduct",
                column: "ApplicableProductsBarcode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponProduct");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CouponCode",
                table: "Products",
                column: "CouponCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Coupons_CouponCode",
                table: "Products",
                column: "CouponCode",
                principalTable: "Coupons",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
