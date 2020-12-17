using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class RemoveCartState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShoppingLists_ContentsId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Carts_CartId",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_CartId",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ContentsId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "SubtotalPrice",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "ContentsId",
                table: "Carts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "ShoppingLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubtotalPrice",
                table: "ShoppingLists",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingLists",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ContentsId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_CartId",
                table: "ShoppingLists",
                column: "CartId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Carts_CartId",
                table: "ShoppingLists",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
