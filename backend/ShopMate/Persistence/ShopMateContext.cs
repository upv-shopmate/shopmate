using Microsoft.EntityFrameworkCore;

namespace ShopMate.Persistence
{
    public class ShopMateContext : DbContext
    {
        public ShopMateContext(DbContextOptions<ShopMateContext> options) : base(options)
        { }

        // public DbSet<MODEL> TABLE_NAME { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API here
        }
    }
}
