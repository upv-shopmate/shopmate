using ShopMate.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Persistence
{
    public interface IShopMateRepository
    {
        public IProductRepository Products { get; }

        public IBrandRepository Brands { get; }

        public ICategoryRepository Categories { get; }

        public IStoreRepository Stores { get; }

        public IShoppingListRepository ShoppingLists { get; }

        public ICartRepository Carts { get; }

        public IUserRepository Users { get; }

        public ICouponRepository Coupons { get; }

        public bool SaveChanges();
    }

    public interface IProductRepository : IAsyncRepository<Product>
    {
        public Product? GetByBarcode(string barcode)
        {
            if (Gtin14.TryFromStandardBarcode(barcode, out var gtin14))
            {
                return GetAll().FirstOrDefault(p => p.Barcode == gtin14!);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Product> SearchByQuery(string query, int page, int itemsPerPage, out bool hasNext);
    }

    public interface IBrandRepository : IAsyncRepository<Brand>
    { }

    public interface ICategoryRepository : IAsyncRepository<Category>
    { }

    public interface IStoreRepository : IAsyncRepository<Store>
    { }

    public interface IShoppingListRepository : IAsyncRepository<ShoppingList>
    { }

    public interface ICartRepository : IAsyncRepository<Cart>
    { }

    public interface IUserRepository : IAsyncRepository<User>
    { }

    public interface ICouponRepository : IAsyncRepository<Coupon>
    { }
}
