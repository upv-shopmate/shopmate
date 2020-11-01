using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class CartContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentsId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ContentsId",
                table: "Carts",
                column: "ContentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ShoppingLists_ContentsId",
                table: "Carts",
                column: "ContentsId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShoppingLists_ContentsId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ContentsId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ContentsId",
                table: "Carts");
        }
    }
}
