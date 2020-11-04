using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class LazyLoading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceModifierBreakdown");

            migrationBuilder.CreateTable(
                name: "ShoppingListEntry",
                columns: table => new
                {
                    ShoppingListId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemBarcode = table.Column<string>(type: "char(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListEntry", x => new { x.ShoppingListId, x.Id });
                    table.ForeignKey(
                        name: "FK_ShoppingListEntry_Products_ItemBarcode",
                        column: x => x.ItemBarcode,
                        principalTable: "Products",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListEntry_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListEntry_ItemBarcode",
                table: "ShoppingListEntry",
                column: "ItemBarcode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListEntry");

            migrationBuilder.CreateTable(
                name: "PriceModifierBreakdown",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceModifierBreakdown", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceModifierBreakdown_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifierBreakdown_ShoppingListId",
                table: "PriceModifierBreakdown",
                column: "ShoppingListId");
        }
    }
}
