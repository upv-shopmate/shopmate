using Microsoft.EntityFrameworkCore;
using ShopMate.Models;

namespace ShopMate.Persistence.Relational
{
    internal class RelationalShopMateRepository : IShopMateRepository
    {
        private readonly ShopMateContext context;

        public RelationalShopMateRepository(ShopMateContext context)
        {
            this.context = context;

            Products = new RelationalProductRepository(context.Products);
            Brands = new RelationalBrandRepository(context.Brands);
            Categories = new RelationalCategoryRepository(context.Categories);
            Labels = new RelationalLabelRepository(context.Labels);
            Stores = new RelationalStoreRepository(context.Stores);
        }

        public IProductRepository Products { get; }

        public IBrandRepository Brands { get; }

        public ICategoryRepository Categories { get; }

        public ILabelRepository Labels { get; }

        public IStoreRepository Stores { get; }

        public bool SaveChanges() => context.SaveChanges() >= 0;
    }

    internal class RelationalProductRepository : RelationalRepository<Product>, IProductRepository
    {
        public RelationalProductRepository(DbSet<Product> set) : base(set)
        { }
    }

    internal class RelationalBrandRepository : RelationalRepository<Brand>, IBrandRepository
    {
        public RelationalBrandRepository(DbSet<Brand> set) : base(set)
        { }
    }

    internal class RelationalCategoryRepository : RelationalRepository<Category>, ICategoryRepository
    {
        public RelationalCategoryRepository(DbSet<Category> set) : base(set)
        { }
    }

    internal class RelationalLabelRepository : RelationalRepository<Label>, ILabelRepository
    {
        public RelationalLabelRepository(DbSet<Label> set) : base(set)
        { }
    }

    internal class RelationalStoreRepository : RelationalRepository<Store>, IStoreRepository
    {
        public RelationalStoreRepository(DbSet<Store> set) : base(set)
        { }
    }
}
