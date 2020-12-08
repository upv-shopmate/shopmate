using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopMate.Models;
using ShopMate.Persistence.Relational;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PopulateDb
{
    internal class Program
    {
        private const string BASE_DIR = "data";
        private const string PRODUCTS_DIR = "products";
        private const string MAP_FILE = "map.json";
        private const string USERS_FILE = "users.json";

        private const int MAX_STOCK = 9000;
        private const int MAX_TIMES_SOLD = 6070;

        readonly static Random rng = new Random();

        readonly static ISet<Category> categoriesCache = new HashSet<Category>();
        readonly static ISet<Brand> brandsCache = new HashSet<Brand>();

        private static void Main(string[] args)
        {
            var path = Path.GetFullPath(args.ElementAtOrDefault(0) ?? BASE_DIR);
            Console.WriteLine($"Data file directory: {path}\n");

            Console.WriteLine(">> Reading data...");
            var products = ReadAllProductFiles(Path.Combine(path, PRODUCTS_DIR));
            var users = ReadUsersFile(Path.Combine(path, USERS_FILE));
            var map = ReadMapFile(Path.Combine(path, MAP_FILE));

            Console.WriteLine(">> Populating with random values...");
            FillWithRandomValues(products);

            Console.WriteLine(">> Seeding database...");
            var insertedCount = SeedDatabase("Mercadona", "EUR", products, users, map);
            Console.WriteLine($"-- Inserted {insertedCount} products.");

            Console.WriteLine(">> Done.");
        }

        private static ICollection<ProductJsonDto> ReadAllProductFiles(string path)
        {
            var products = new List<ProductJsonDto>();

            var files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                ReadProductFile(file, products);
            }

            return products;
        }

        private static void ReadProductFile(string path, ICollection<ProductJsonDto> products)
        {
            using var stream = new StreamReader(path);
            using var reader = new JsonTextReader(stream)
            {
                SupportMultipleContent = true
            };

            // done like this because we expect files to be large
            var serializer = new JsonSerializer();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    var product = serializer.Deserialize<ProductJsonDto>(reader);
                    products.Add(product);
                }
            }
        }

        private static ICollection<UserJsonDto> ReadUsersFile(string path)
        {
            using var stream = new StreamReader(path);
            return JsonConvert.DeserializeObject<List<UserJsonDto>>(stream.ReadToEnd());
        }

        private static int[][] ReadMapFile(string path)
        {
            using var stream = new StreamReader(path);
            return JsonConvert.DeserializeObject<int[][]>(stream.ReadToEnd());
        }

        private static void FillWithRandomValues(ICollection<ProductJsonDto> products)
        {
            foreach (var product in products)
            {
                product.AvailableStock = (uint)rng.Next(0, MAX_STOCK);
                product.TimesSold = (uint)rng.Next(0, MAX_TIMES_SOLD);
            }
        }

        private static uint SeedDatabase(string shopName, string currency, ICollection<ProductJsonDto> products, ICollection<UserJsonDto> users, int[][] map)
        {
            uint count = 0;

            var optionsBuilder = new DbContextOptionsBuilder<ShopMateContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ShopMateContext;Trusted_Connection=True;");
            using var db = new ShopMateContext(optionsBuilder.Options);

            var store = new Store(shopName, currency)
            {
                Map = map
            };
            db.Set<Store>().Add(store);

            var cart = new Cart
            {
                Active = true
            };
            db.Set<Cart>().Add(cart);
            cart.Owner = store;

            var vat21 = new PriceModifier(PriceModifierCode.Vat, "", 0.21M, PriceModifierKind.Multiplicative);

            foreach (var userDto in users)
            {
                var user = new User(userDto.Name, userDto.Email, userDto.Phone, userDto.Password);
                db.Set<User>().Add(user);
            }

            foreach (var product in products)
            {
                if (InsertProduct(db, store, vat21, product))
                {
                    count++;
                }
            }

            db.SaveChanges();

            return count;
        }

        private static bool InsertProduct(ShopMateContext db, Store vendor, PriceModifier modifier, ProductJsonDto entry)
        {
            Gtin14? barcode = null;
            if (!(entry.Barcode is null) && !Gtin14.TryFromStandardBarcode(entry.Barcode, out barcode))
            {
                Console.WriteLine($"-- Skipping product with invalid barcode: {entry.ProductId} {entry.Name}");
                return false;
            }

            if (!(db.Set<Product>().Find(entry.ProductId) is null))
            {
                Console.WriteLine($"-- Not modifying product already present: {entry.ProductId} {entry.Name}");
                return false;
            }

            var category = MakeCategory(db.Categories, entry.Category, entry.SuperCategory);
            categoriesCache.Add(category);
            if (!(category.Parent is null))
            {
                categoriesCache.Add(category.Parent);
            }

            var product = new Product(
                barcode,
                entry.Name.LimitLength(120),
                entry.Weight,
                entry.Volume,
                entry.OriginCountry?.LimitLength(2),
                entry.Price,
                entry.Images,
                entry.AvailableStock,
                (uint)entry.TimesSold!)
            {
                Id = entry.ProductId,
            };
            product.Categories.Add(category);

            product.Vendors.Add(vendor);

            if (entry.Brands != null)
            {
                foreach (var name in entry.Brands)
                {
                    var brand = db.Brands.MatchingOrNew(brandsCache, new Brand(name.LimitLength(50), new List<string>(), null));
                    product.Brands.Add(brand);
                }
            }

            product.PriceModifiers.Add(modifier);
            modifier.Products.Add(product);

            db.Set<Product>().Add(product);
            return true;
        }

        private static Category MakeCategory(DbSet<Category> categories, string categoryName, string? superCategoryName)
        {
            var category = categories.MatchingOrNew(categoriesCache, new Category(categoryName));

            if (!(superCategoryName is null) && category.Parent is null)
            {
                category.Parent = categories.MatchingOrNew(categoriesCache, new Category(superCategoryName));
            }

            return category;
        }
    }
}
