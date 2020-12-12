using Microsoft.EntityFrameworkCore;
using ShopMate.Models;
using System.Collections.Generic;
using System.Linq;

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
            Stores = new RelationalStoreRepository(context.Stores);
            ShoppingLists = new RelationalShoppingListRepository(context.ShoppingLists);
            Carts = new RelationalCartRepository(context.Carts);
            Users = new RelationalUserRepository(context.Users);
            Coupons = new RelationalCouponRepository(context.Coupons);
        }

        public IProductRepository Products { get; }

        public IBrandRepository Brands { get; }

        public ICategoryRepository Categories { get; }

        public IStoreRepository Stores { get; }

        public IShoppingListRepository ShoppingLists { get; }

        public ICartRepository Carts { get; }

        public IUserRepository Users { get; }

        public ICouponRepository Coupons { get; }

        public bool SaveChanges() => context.SaveChanges() > 0;
    }
    internal class RelationalProductRepository : RelationalRepository<Product>, IProductRepository
    {
        public RelationalProductRepository(DbSet<Product> set) : base(set)
        { }

        public IEnumerable<Product> SearchByQuery(string query, int page, int itemsPerPage, out bool hasNext)
        {
            var tokens = query.Trim().ToLower().Split(' ');

            var products = Set
                            .Include(p => p.Brands)
                            .Include(p => p.Categories)
                            .AsEnumerable()
                            .Where(p =>
                                (p.Barcode.HasValue && tokens.Contains(p.Barcode.Value.Value))
                                || tokens.Intersect(p.Name.ToLower().Split()).Any()
                                || tokens.Intersect(p.Brands.SelectMany(b => b.Aliases.Select(a => a.ToLower()).Append(b.Name.ToLower()))).Any()
                                || tokens.Intersect(p.Categories.Select(c => c.Name)).Any()
                                || tokens.Contains(p.Weight + "g")
                            )
                            .OrderBy(p => p.Id)
                            .Skip(page * itemsPerPage)
                            .Take(itemsPerPage + 1)
                            .ToList();

            hasNext = products.Count > itemsPerPage;

            return products.SkipLast(1);
        }

        public override IQueryable<Product> GetAll()
            => Set
                .Include(p => p.Brands)
                .Include(p => p.Categories)
                .ThenInclude(c => c.Parent)     // no recursive load: requirements say at most 2 category levels
                .Include(p => p.Positions)
                .Include(p => p.Vendors)
                .Include(p => p.PriceModifiers);
    }

    internal class RelationalBrandRepository : RelationalRepository<Brand>, IBrandRepository
    {
        public RelationalBrandRepository(DbSet<Brand> set) : base(set)
        { }

        public override IQueryable<Brand> GetAll()
            => Set
                .Include(p => p.Products);
    }

    internal class RelationalCategoryRepository : RelationalRepository<Category>, ICategoryRepository
    {
        public RelationalCategoryRepository(DbSet<Category> set) : base(set)
        { }

        public override IQueryable<Category> GetAll()
            => Set
                .Include(c => c.Products)
                .Include(c => c.Parent);
    }

    internal class RelationalStoreRepository : RelationalRepository<Store>, IStoreRepository
    {
        public RelationalStoreRepository(DbSet<Store> set) : base(set)
        { }

        public override IQueryable<Store> GetAll()
            => Set
                .Include(s => s.Products);
    }

    internal class RelationalShoppingListRepository : RelationalRepository<ShoppingList>, IShoppingListRepository
    {
        public RelationalShoppingListRepository(DbSet<ShoppingList> set) : base(set)
        { }

        public override IQueryable<ShoppingList> GetAll()
            => Set
                .Include(l => l.Entries)
                .ThenInclude(e => e.Item)
                .ThenInclude(p => p.PriceModifiers);
    }

    internal class RelationalCartRepository : RelationalRepository<Cart>, ICartRepository
    {
        public RelationalCartRepository(DbSet<Cart> set) : base(set)
        { }

        public override IQueryable<Cart> GetAll()
            => Set
                .Include(c => c.Contents)
                .ThenInclude(l => l.Entries);
    }

    internal class RelationalUserRepository : RelationalRepository<User>, IUserRepository
    {
        public RelationalUserRepository(DbSet<User> set) : base(set)
        { }

        public override IQueryable<User> GetAll()
            => Set;
    }

    internal class RelationalCouponRepository : RelationalRepository<Coupon>, ICouponRepository
    {
        public RelationalCouponRepository(DbSet<Coupon> set) : base(set)
        { }

        public override IQueryable<Coupon> GetAll()
            => Set
                .Include(c => c.Effects)
                .Include(c => c.ApplicableProducts)
                .Include(c => c.Store);
    }
}
