using ShopMate.Models;
using System.Collections;
using System.Collections.Generic;

namespace ShopMate.Persistence
{
    public interface IShopMateRepository
    {
        public IProductRepository Products { get; }

        public IBrandRepository Brands { get; }

        public ICategoryRepository Categories { get; }

        public ILabelRepository Labels { get; }

        public IStoreRepository Stores { get; }

        public IShoppingListRepository ShoppingLists { get; }

        public ICartRepository Carts { get; }

        public bool SaveChanges();
    }

    public interface IProductRepository : IAsyncRepository<Product>
    {
        public Product? GetByBarcode(string barcode)
        {
            if (Gtin14.TryFromStandardBarcode(barcode, out Gtin14? gtin14))
            {
                return GetById(gtin14!);
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

    public interface ILabelRepository : IAsyncRepository<Label>
    { }

    public interface IStoreRepository : IAsyncRepository<Store>
    { }

    public interface IShoppingListRepository : IAsyncRepository<ShoppingList> 
    { }

    public interface ICartRepository : IAsyncRepository<Cart> 
    { }
}
