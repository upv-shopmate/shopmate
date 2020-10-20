using Newtonsoft.Json;
using ShopMate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopulateDb
{
    internal class Program
    {
        private const string DEFAULT_INTERPRETER = "python";
        private const string BASE_DIR = "scrapOpenFoodFacts/";

        private static async Task Main(string[] args)
        {
            var interpreter = args.ElementAtOrDefault(0) ?? DEFAULT_INTERPRETER;
            var limit = args.ElementAtOrDefault(1) is null ? "" : args[1];
            Console.WriteLine($"{interpreter} {limit}");

            Console.WriteLine(">> Running scraping process...");
            RunScript(interpreter, limit);

            Console.WriteLine(">> Reading data...");
            var data = await ReadData();

            Console.WriteLine(">> Seeding database...");
            var insertedCount = SeedDatabase("Mercadona", "EUR", data);
            Console.WriteLine($"-- Inserted {insertedCount} products.");

            Console.WriteLine(">> Done.");
        }

        private static void RunScript(string interpreter, string limit)
        {
            using var process = new CommandLineProcess(interpreter, $"{Path.Combine(BASE_DIR, "scrap.py")} {limit}");
            process.Start();

            Console.WriteLine($"-- (PID: {process.Id}) {process.ProcessName}");
            process.OutputDataReceived += (_, args) => { Console.WriteLine(args.Data); };
            process.ErrorDataReceived += (_, args) => { Console.Error.WriteLine(args.Data); };

            process.WaitForExit();
        }

        private static async Task<IDictionary<string, ProductJsonDto>> ReadData()
        {
            var reader = new StreamReader("products.json");
            var json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, ProductJsonDto>>(json);
        }

        private static uint SeedDatabase(string shopName, string currency, IDictionary<string, ProductJsonDto> data)
        {
            uint count = 0;

            using var db = new ShopMateContext();

            var store = new Store(shopName, currency);
            db.Set<Store>().Add(store);

            foreach (var entry in data)
            {
                InsertProduct(db, store, entry);
                count++;
            }

            db.SaveChanges();

            return count;
        }

        private static void InsertProduct(ShopMateContext db, Store vendor, KeyValuePair<string, ProductJsonDto> entry)
        {
            Gtin14 barcode;
            try
            {
                barcode = Gtin14.FromStandardBarcode(entry.Key);
            } 
            catch (ArgumentException)
            {
                Console.WriteLine($"-- Skipping product with invalid barcode: {entry.Key}");
                return;
            }

            var dto = entry.Value;

            if (!(db.Set<Product>().Find(barcode) is null))
            {
                return;
            }

            var product = new Product(
                barcode,
                dto.Name.LimitLength(120),
                dto.Weight,
                dto.Volume,
                dto.Units,
                dto.OriginCountry?.LimitLength(2),
                dto.Edible,
                dto.Price,
                dto.Pictures,
                dto.AvailableStock,
                dto.TimesSold);

            product.Vendors.Add(vendor);

            if (dto.Brands != null)
            {
                foreach (var name in dto.Brands)
                {
                    product.Brands.Add(new Brand(name.LimitLength(50), new List<string>(), null));
                }
            }

            if (dto.Categories != null)
            {
                foreach (var name in dto.Categories)
                {
                    product.Categories.Add(new Category(name.LimitLength(50)));
                }
            }

            if (dto.Labels != null)
            {
                foreach (var name in dto.Labels)
                {
                    product.Labels.Add(new Label(name.LimitLength(50)));
                }
            }

            product.PriceModifiers.Add(new PriceModifier(PriceModifierCode.Vat, "", 0.21M, PriceModifierKind.Multiplicative));

            db.Set<Product>().Add(product);
        }
    }

    public static class StringExtensions
    {
        public static string LimitLength(this string source, int maxLength)
        {
            if (source.Length <= maxLength)
            {
                return source;
            }

            return source.Substring(0, maxLength);
        }
    }
}
