﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopMate.Persistence.Relational;

namespace ShopMate.Migrations
{
    [DbContext(typeof(ShopMateContext))]
    [Migration("20201115110719_UserCoupons")]
    partial class UserCoupons
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BrandProduct", b =>
                {
                    b.Property<int>("BrandsId")
                        .HasColumnType("int");

                    b.Property<string>("ProductsBarcode")
                        .HasColumnType("char(14)");

                    b.HasKey("BrandsId", "ProductsBarcode");

                    b.HasIndex("ProductsBarcode");

                    b.ToTable("BrandProduct");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<string>("ProductsBarcode")
                        .HasColumnType("char(14)");

                    b.HasKey("CategoriesId", "ProductsBarcode");

                    b.HasIndex("ProductsBarcode");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("CouponProduct", b =>
                {
                    b.Property<string>("AffectedByCouponsCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicableProductsBarcode")
                        .HasColumnType("char(14)");

                    b.HasKey("AffectedByCouponsCode", "ApplicableProductsBarcode");

                    b.HasIndex("ApplicableProductsBarcode");

                    b.ToTable("CouponProduct");
                });

            modelBuilder.Entity("CouponUser", b =>
                {
                    b.Property<string>("OwnedCouponsCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OwnersId")
                        .HasColumnType("int");

                    b.HasKey("OwnedCouponsCode", "OwnersId");

                    b.HasIndex("OwnersId");

                    b.ToTable("CouponUser");
                });

            modelBuilder.Entity("LabelProduct", b =>
                {
                    b.Property<int>("LabelsId")
                        .HasColumnType("int");

                    b.Property<string>("ProductsBarcode")
                        .HasColumnType("char(14)");

                    b.HasKey("LabelsId", "ProductsBarcode");

                    b.HasIndex("ProductsBarcode");

                    b.ToTable("LabelProduct");
                });

            modelBuilder.Entity("PriceModifierProduct", b =>
                {
                    b.Property<int>("PriceModifiersId")
                        .HasColumnType("int");

                    b.Property<string>("ProductsBarcode")
                        .HasColumnType("char(14)");

                    b.HasKey("PriceModifiersId", "ProductsBarcode");

                    b.HasIndex("ProductsBarcode");

                    b.ToTable("PriceModifierProduct");
                });

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.Property<string>("ProductsBarcode")
                        .HasColumnType("char(14)");

                    b.Property<int>("VendorsId")
                        .HasColumnType("int");

                    b.HasKey("ProductsBarcode", "VendorsId");

                    b.HasIndex("VendorsId");

                    b.ToTable("ProductStore");
                });

            modelBuilder.Entity("ShopMate.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Aliases")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("ShopMate.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("ContentsId")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentsId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ShopMate.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ShopMate.Models.Coupon", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.HasIndex("StoreId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("ShopMate.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("ShopMate.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ProductBarcode")
                        .HasColumnType("char(14)");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductBarcode");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("ShopMate.Models.PriceModifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CouponCode");

                    b.ToTable("PriceModifier");
                });

            modelBuilder.Entity("ShopMate.Models.Product", b =>
                {
                    b.Property<string>("Barcode")
                        .HasColumnType("char(14)");

                    b.Property<long?>("AvailableStock")
                        .HasColumnType("bigint");

                    b.Property<bool>("Edible")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("OriginCountry")
                        .HasColumnType("char(2)");

                    b.Property<string>("Pictures")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<long>("TimesSold")
                        .HasColumnType("bigint");

                    b.Property<int?>("Units")
                        .HasColumnType("int");

                    b.Property<double?>("Volume")
                        .HasColumnType("float");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Barcode");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopMate.Models.ShoppingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("SubtotalPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("ShopMate.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("ShopMate.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MoneySpent")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BrandProduct", b =>
                {
                    b.HasOne("ShopMate.Models.Brand", null)
                        .WithMany()
                        .HasForeignKey("BrandsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsBarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("ShopMate.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsBarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CouponProduct", b =>
                {
                    b.HasOne("ShopMate.Models.Coupon", null)
                        .WithMany()
                        .HasForeignKey("AffectedByCouponsCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ApplicableProductsBarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CouponUser", b =>
                {
                    b.HasOne("ShopMate.Models.Coupon", null)
                        .WithMany()
                        .HasForeignKey("OwnedCouponsCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.User", null)
                        .WithMany()
                        .HasForeignKey("OwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LabelProduct", b =>
                {
                    b.HasOne("ShopMate.Models.Label", null)
                        .WithMany()
                        .HasForeignKey("LabelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsBarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PriceModifierProduct", b =>
                {
                    b.HasOne("ShopMate.Models.PriceModifier", null)
                        .WithMany()
                        .HasForeignKey("PriceModifiersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsBarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsBarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopMate.Models.Store", null)
                        .WithMany()
                        .HasForeignKey("VendorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopMate.Models.Cart", b =>
                {
                    b.HasOne("ShopMate.Models.ShoppingList", "Contents")
                        .WithMany()
                        .HasForeignKey("ContentsId");

                    b.HasOne("ShopMate.Models.Store", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Contents");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ShopMate.Models.Coupon", b =>
                {
                    b.HasOne("ShopMate.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ShopMate.Models.Position", b =>
                {
                    b.HasOne("ShopMate.Models.Product", null)
                        .WithMany("Positions")
                        .HasForeignKey("ProductBarcode");
                });

            modelBuilder.Entity("ShopMate.Models.PriceModifier", b =>
                {
                    b.HasOne("ShopMate.Models.Coupon", null)
                        .WithMany("Effects")
                        .HasForeignKey("CouponCode");
                });

            modelBuilder.Entity("ShopMate.Models.ShoppingList", b =>
                {
                    b.HasOne("ShopMate.Models.Cart", null)
                        .WithMany("TrackedLists")
                        .HasForeignKey("CartId");

                    b.OwnsMany("ShopMate.Models.ShoppingListEntry", "Entries", b1 =>
                        {
                            b1.Property<int>("ShoppingListId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .UseIdentityColumn();

                            b1.Property<string>("ItemBarcode")
                                .IsRequired()
                                .HasColumnType("char(14)");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("ShoppingListId", "Id");

                            b1.HasIndex("ItemBarcode");

                            b1.ToTable("ShoppingListEntry");

                            b1.HasOne("ShopMate.Models.Product", "Item")
                                .WithMany()
                                .HasForeignKey("ItemBarcode")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("ShoppingListId");

                            b1.Navigation("Item");
                        });

                    b.Navigation("Entries");
                });

            modelBuilder.Entity("ShopMate.Models.Cart", b =>
                {
                    b.Navigation("TrackedLists");
                });

            modelBuilder.Entity("ShopMate.Models.Coupon", b =>
                {
                    b.Navigation("Effects");
                });

            modelBuilder.Entity("ShopMate.Models.Product", b =>
                {
                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
