using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Migrations
{
    public partial class NewProductScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandProduct_Products_ProductsBarcode",
                table: "BrandProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsBarcode",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CouponProduct_Products_ApplicableProductsBarcode",
                table: "CouponProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_Products_ProductBarcode",
                table: "Position");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceModifierProduct_Products_ProductsBarcode",
                table: "PriceModifierProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStore_Products_ProductsBarcode",
                table: "ProductStore");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemBarcode",
                table: "ShoppingListEntry");

            migrationBuilder.DropTable(
                name: "LabelProduct");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListEntry_ItemBarcode",
                table: "ShoppingListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStore",
                table: "ProductStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceModifierProduct",
                table: "PriceModifierProduct");

            migrationBuilder.DropIndex(
                name: "IX_PriceModifierProduct_ProductsBarcode",
                table: "PriceModifierProduct");

            migrationBuilder.DropIndex(
                name: "IX_Position_ProductBarcode",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CouponProduct",
                table: "CouponProduct");

            migrationBuilder.DropIndex(
                name: "IX_CouponProduct_ApplicableProductsBarcode",
                table: "CouponProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_CategoryProduct_ProductsBarcode",
                table: "CategoryProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandProduct",
                table: "BrandProduct");

            migrationBuilder.DropIndex(
                name: "IX_BrandProduct_ProductsBarcode",
                table: "BrandProduct");

            migrationBuilder.DropColumn(
                name: "ItemBarcode",
                table: "ShoppingListEntry");

            migrationBuilder.DropColumn(
                name: "ProductsBarcode",
                table: "ProductStore");

            migrationBuilder.DropColumn(
                name: "Edible",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Units",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductsBarcode",
                table: "PriceModifierProduct");

            migrationBuilder.DropColumn(
                name: "ProductBarcode",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "ApplicableProductsBarcode",
                table: "CouponProduct");

            migrationBuilder.DropColumn(
                name: "ProductsBarcode",
                table: "CategoryProduct");

            migrationBuilder.DropColumn(
                name: "ProductsBarcode",
                table: "BrandProduct");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "ShoppingListEntry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "ProductStore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "char(14)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(14)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "PriceModifierProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Position",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicableProductsId",
                table: "CouponProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "CategoryProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuperiorId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "BrandProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStore",
                table: "ProductStore",
                columns: new[] { "ProductsId", "VendorsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceModifierProduct",
                table: "PriceModifierProduct",
                columns: new[] { "PriceModifiersId", "ProductsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CouponProduct",
                table: "CouponProduct",
                columns: new[] { "AffectedByCouponsCode", "ApplicableProductsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandProduct",
                table: "BrandProduct",
                columns: new[] { "BrandsId", "ProductsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListEntry_ItemId",
                table: "ShoppingListEntry",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifierProduct_ProductsId",
                table: "PriceModifierProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ProductId",
                table: "Position",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponProduct_ApplicableProductsId",
                table: "CouponProduct",
                column: "ApplicableProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SuperiorId",
                table: "Categories",
                column: "SuperiorId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_ProductsId",
                table: "BrandProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandProduct_Products_ProductsId",
                table: "BrandProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_SuperiorId",
                table: "Categories",
                column: "SuperiorId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CouponProduct_Products_ApplicableProductsId",
                table: "CouponProduct",
                column: "ApplicableProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Products_ProductId",
                table: "Position",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceModifierProduct_Products_ProductsId",
                table: "PriceModifierProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStore_Products_ProductsId",
                table: "ProductStore",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemId",
                table: "ShoppingListEntry",
                column: "ItemId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandProduct_Products_ProductsId",
                table: "BrandProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_SuperiorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CouponProduct_Products_ApplicableProductsId",
                table: "CouponProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_Products_ProductId",
                table: "Position");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceModifierProduct_Products_ProductsId",
                table: "PriceModifierProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStore_Products_ProductsId",
                table: "ProductStore");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemId",
                table: "ShoppingListEntry");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListEntry_ItemId",
                table: "ShoppingListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStore",
                table: "ProductStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceModifierProduct",
                table: "PriceModifierProduct");

            migrationBuilder.DropIndex(
                name: "IX_PriceModifierProduct_ProductsId",
                table: "PriceModifierProduct");

            migrationBuilder.DropIndex(
                name: "IX_Position_ProductId",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CouponProduct",
                table: "CouponProduct");

            migrationBuilder.DropIndex(
                name: "IX_CouponProduct_ApplicableProductsId",
                table: "CouponProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SuperiorId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandProduct",
                table: "BrandProduct");

            migrationBuilder.DropIndex(
                name: "IX_BrandProduct_ProductsId",
                table: "BrandProduct");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ShoppingListEntry");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "ProductStore");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "PriceModifierProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "ApplicableProductsId",
                table: "CouponProduct");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropColumn(
                name: "SuperiorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "BrandProduct");

            migrationBuilder.AddColumn<string>(
                name: "ItemBarcode",
                table: "ShoppingListEntry",
                type: "char(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductsBarcode",
                table: "ProductStore",
                type: "char(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "char(14)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(14)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Edible",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Units",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductsBarcode",
                table: "PriceModifierProduct",
                type: "char(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductBarcode",
                table: "Position",
                type: "char(14)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicableProductsBarcode",
                table: "CouponProduct",
                type: "char(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductsBarcode",
                table: "CategoryProduct",
                type: "char(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductsBarcode",
                table: "BrandProduct",
                type: "char(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStore",
                table: "ProductStore",
                columns: new[] { "ProductsBarcode", "VendorsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Barcode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceModifierProduct",
                table: "PriceModifierProduct",
                columns: new[] { "PriceModifiersId", "ProductsBarcode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CouponProduct",
                table: "CouponProduct",
                columns: new[] { "AffectedByCouponsCode", "ApplicableProductsBarcode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryProduct",
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsBarcode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandProduct",
                table: "BrandProduct",
                columns: new[] { "BrandsId", "ProductsBarcode" });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabelProduct",
                columns: table => new
                {
                    LabelsId = table.Column<int>(type: "int", nullable: false),
                    ProductsBarcode = table.Column<string>(type: "char(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelProduct", x => new { x.LabelsId, x.ProductsBarcode });
                    table.ForeignKey(
                        name: "FK_LabelProduct_Labels_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabelProduct_Products_ProductsBarcode",
                        column: x => x.ProductsBarcode,
                        principalTable: "Products",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListEntry_ItemBarcode",
                table: "ShoppingListEntry",
                column: "ItemBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_PriceModifierProduct_ProductsBarcode",
                table: "PriceModifierProduct",
                column: "ProductsBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ProductBarcode",
                table: "Position",
                column: "ProductBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_CouponProduct_ApplicableProductsBarcode",
                table: "CouponProduct",
                column: "ApplicableProductsBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsBarcode",
                table: "CategoryProduct",
                column: "ProductsBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_ProductsBarcode",
                table: "BrandProduct",
                column: "ProductsBarcode");

            migrationBuilder.CreateIndex(
                name: "IX_LabelProduct_ProductsBarcode",
                table: "LabelProduct",
                column: "ProductsBarcode");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandProduct_Products_ProductsBarcode",
                table: "BrandProduct",
                column: "ProductsBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsBarcode",
                table: "CategoryProduct",
                column: "ProductsBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CouponProduct_Products_ApplicableProductsBarcode",
                table: "CouponProduct",
                column: "ApplicableProductsBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Products_ProductBarcode",
                table: "Position",
                column: "ProductBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceModifierProduct_Products_ProductsBarcode",
                table: "PriceModifierProduct",
                column: "ProductsBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStore_Products_ProductsBarcode",
                table: "ProductStore",
                column: "ProductsBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListEntry_Products_ItemBarcode",
                table: "ShoppingListEntry",
                column: "ItemBarcode",
                principalTable: "Products",
                principalColumn: "Barcode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
