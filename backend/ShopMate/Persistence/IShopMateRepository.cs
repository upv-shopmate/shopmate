using ShopMate.Models;

namespace ShopMate.Persistence
{
    public interface IShopMateRepository
    {
        public IProductRepository Products { get; }

        public IBrandRepository Brands { get; }

        public ICategoryRepository Categories { get; }

        public ILabelRepository Labels { get; }

        public IStoreRepository Stores { get; }

        public bool SaveChanges();
    }

    public interface IProductRepository : IAsyncRepository<Product>
    {
        public Product? GetByBarcode(string barcode) => GetById(Gtin14.FromStandardBarcode(barcode));
    }

    public interface IBrandRepository : IAsyncRepository<Brand>
    { }

    public interface ICategoryRepository : IAsyncRepository<Category>
    { }

    public interface ILabelRepository : IAsyncRepository<Label>
    { }

    public interface IStoreRepository : IAsyncRepository<Store>
    { }
}
