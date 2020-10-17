using Microsoft.EntityFrameworkCore;
using ShopMate.Models;
using ShopMate.Persistence.Relational;

namespace PopulateDb
{
    internal class ShopMateContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShopMateContext;Trusted_Connection=True;MultipleActiveResultSets=true");

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
        }
    }
}
