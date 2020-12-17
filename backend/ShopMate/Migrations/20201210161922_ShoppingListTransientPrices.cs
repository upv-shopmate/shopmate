using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class ShoppingListTransientPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubtotalPrice",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
