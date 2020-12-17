using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class DeletedOwnedAndNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemId",
                table: "ShoppingListEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListEntry_ShoppingLists_ShoppingListId",
                table: "ShoppingListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListEntry",
                table: "ShoppingListEntry");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ShoppingListEntry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingListId",
                table: "ShoppingListEntry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingListEntryId",
                table: "PriceModifier",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Coupons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingListId",
                table: "Coupons",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListEntry",
                table: "ShoppingListEntry",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListEntry_ShoppingListId",
                table: "ShoppingListEntry",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifier_ShoppingListEntryId",
                table: "PriceModifier",
                column: "ShoppingListEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CartId",
                table: "Coupons",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ShoppingListId",
                table: "Coupons",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Carts_CartId",
                table: "Coupons",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_ShoppingLists_ShoppingListId",
                table: "Coupons",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceModifier_ShoppingListEntry_ShoppingListEntryId",
                table: "PriceModifier",
                column: "ShoppingListEntryId",
                principalTable: "ShoppingListEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemId",
                table: "ShoppingListEntry",
                column: "ItemId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListEntry_ShoppingLists_ShoppingListId",
                table: "ShoppingListEntry",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Carts_CartId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_ShoppingLists_ShoppingListId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceModifier_ShoppingListEntry_ShoppingListEntryId",
                table: "PriceModifier");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemId",
                table: "ShoppingListEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListEntry_ShoppingLists_ShoppingListId",
                table: "ShoppingListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListEntry",
                table: "ShoppingListEntry");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListEntry_ShoppingListId",
                table: "ShoppingListEntry");

            migrationBuilder.DropIndex(
                name: "IX_PriceModifier_ShoppingListEntryId",
                table: "PriceModifier");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CartId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_ShoppingListId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ShoppingListEntryId",
                table: "PriceModifier");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ShoppingListId",
                table: "Coupons");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingListId",
                table: "ShoppingListEntry",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ShoppingListEntry",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListEntry",
                table: "ShoppingListEntry",
                columns: new[] { "ShoppingListId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemId",
                table: "ShoppingListEntry",
                column: "ItemId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListEntry_ShoppingLists_ShoppingListId",
                table: "ShoppingListEntry",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
