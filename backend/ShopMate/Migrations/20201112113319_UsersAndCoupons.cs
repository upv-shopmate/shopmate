using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class UsersAndCoupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "PriceModifier",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneySpent = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Coupons_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Coupons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CouponCode",
                table: "Products",
                column: "CouponCode");

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifier_CouponCode",
                table: "PriceModifier",
                column: "CouponCode");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_StoreId",
                table: "Coupons",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceModifier_Coupons_CouponCode",
                table: "PriceModifier",
                column: "CouponCode",
                principalTable: "Coupons",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Coupons_CouponCode",
                table: "Products",
                column: "CouponCode",
                principalTable: "Coupons",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceModifier_Coupons_CouponCode",
                table: "PriceModifier");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Coupons_CouponCode",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Products_CouponCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_PriceModifier_CouponCode",
                table: "PriceModifier");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "PriceModifier");
        }
    }
}
