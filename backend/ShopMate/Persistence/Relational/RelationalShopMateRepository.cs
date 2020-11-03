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
            Labels = new RelationalLabelRepository(context.Labels);
            Stores = new RelationalStoreRepository(context.Stores);
            ShoppingLists = new RelationalShoppingListRepository(context.ShoppingLists);
            Carts = new RelationalCartRepository(context.Carts);
        }

        public IProductRepository Products { get; }

        public IBrandRepository Brands { get; }

        public ICategoryRepository Categories { get; }

        public ILabelRepository Labels { get; }

        public IStoreRepository Stores { get; }

        public IShoppingListRepository ShoppingLists { get; }

        public ICartRepository Carts { get; }

        public bool SaveChanges() => context.SaveChanges() > 0; //Antes era >= 0
    }
    internal class RelationalProductRepository : RelationalRepository<Product>, IProductRepository
    {
        public RelationalProductRepository(DbSet<Product> set) : base(set)
        { }

        public IEnumerable<Product> SearchByQuery(string query, int page, int itemsPerPage, out bool hasNext)
        {
            var tokens = query.Trim().ToLower().Split(' ');

            var products = GetAll()
                            .Where(p =>
                                tokens.Contains(p.Barcode.Value)
                                || tokens.Intersect(p.Name.ToLower().Split(' ')).Any()
                                || tokens.Intersect(p.Brands.SelectMany(b => b.Aliases.Select(a => a.ToLower()).Append(b.Name.ToLower()))).Any()
                                || tokens.Intersect(p.Categories.Select(c => c.Name)).Any()
                                || tokens.Intersect(p.Labels.Select(l => l.Name)).Any()
                                || tokens.Contains(p.Weight + "g")
                            )
                            .Skip(page * itemsPerPage)
                            .Take(itemsPerPage + 1)
                            .ToList();

            hasNext = products.Count > itemsPerPage;

            return products.SkipLast(1);
        }
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

    internal class RelationalShoppingListRepository : RelationalRepository<ShoppingList>, IShoppingListRepository
    {
        public RelationalShoppingListRepository(DbSet<ShoppingList> set) : base(set)
        { }
    }

    internal class RelationalCartRepository : RelationalRepository<Cart>, ICartRepository
    {
        public RelationalCartRepository(DbSet<Cart> set) : base(set) 
        { }
    }
}
