using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class Añadida_relacion_muchos_a_muchos_de_cupones_usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coupons");

            migrationBuilder.CreateTable(
                name: "CouponUser",
                columns: table => new
                {
                    OwnedCouponsCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnersOfCuponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUser", x => new { x.OwnedCouponsCode, x.OwnersOfCuponId });
                    table.ForeignKey(
                        name: "FK_CouponUser_Coupons_OwnedCouponsCode",
                        column: x => x.OwnedCouponsCode,
                        principalTable: "Coupons",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponUser_Users_OwnersOfCuponId",
                        column: x => x.OwnersOfCuponId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponUser_OwnersOfCuponId",
                table: "CouponUser",
                column: "OwnersOfCuponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Coupons",
                type: "int",
                nullable: true);

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
        }
    }
}
