using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class UserCoupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Coupons_CouponCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CouponCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coupons");

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

            migrationBuilder.CreateTable(
                name: "CouponUser",
                columns: table => new
                {
                    OwnedCouponsCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUser", x => new { x.OwnedCouponsCode, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_CouponUser_Coupons_OwnedCouponsCode",
                        column: x => x.OwnedCouponsCode,
                        principalTable: "Coupons",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponUser_Users_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponProduct_ApplicableProductsBarcode",
                table: "CouponProduct",
                column: "ApplicableProductsBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_CouponUser_OwnersId",
                table: "CouponUser",
                column: "OwnersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponProduct");

            migrationBuilder.DropTable(
                name: "CouponUser");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Coupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CouponCode",
                table: "Products",
                column: "CouponCode");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
