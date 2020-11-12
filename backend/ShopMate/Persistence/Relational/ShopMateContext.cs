using Microsoft.EntityFrameworkCore;
using ShopMate.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PopulateDb")]
namespace ShopMate.Persistence.Relational
{
    internal class ShopMateContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public ShopMateContext(DbContextOptions<ShopMateContext> options) : base(options)
        { }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(o =>
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                 .EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Barcode)
                .HasColumnType("char(14)")
                .HasConversion(
                    v => v.Value,
                    v => new Gtin14(v));

            SetupPrimitiveCollectionTypes(modelBuilder);
        }

        protected void SetupPrimitiveCollectionTypes(ModelBuilder modelBuilder)
        {
            // TODO: optimize after deciding a concrete type for pictures
            modelBuilder.Entity<Product>()
                .Property(p => p.Pictures)
                .HasJsonConversion();

            modelBuilder.Entity<Brand>()
                .Property(b => b.Aliases)
                .HasJsonConversion();

            modelBuilder.Entity<Store>()
                .Property(s => s.Map)
                .HasJsonConversion();
        }
    }
}
