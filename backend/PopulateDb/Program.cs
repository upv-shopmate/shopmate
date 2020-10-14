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
        private const uint DEFAULT_LIMIT = 216;
        private const string BASE_DIR = "scrapOpenFoodFacts/";

        private static async Task Main(string[] args)
        {
            var interpreter = args.ElementAtOrDefault(0) ?? DEFAULT_INTERPRETER;
            var limit = args.ElementAtOrDefault(1) is null ? DEFAULT_LIMIT : uint.Parse(args[1]);
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

        private static void RunScript(string interpreter, uint limit)
        {
            using var process = new CommandLineProcess(interpreter, $"{Path.Combine(BASE_DIR, "scrap.py")} {limit}");
            process.Start();

            Console.WriteLine($"-- (PID: {process.Id}) {process.ProcessName}");
            process.OutputDataReceived += (_, args) => { Console.WriteLine(args.Data); };
            process.ErrorDataReceived += (_, args) => { Console.Error.WriteLine(args.Data); };

            process.WaitForExit();
        }

        private static async Task<IDictionary<string, ProductJsonDAO>> ReadData()
        {
            var reader = new StreamReader(Path.Combine(BASE_DIR, "products.json"));
            var json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, ProductJsonDAO>>(json);
        }

        private static uint SeedDatabase(string shopName, string currency, IDictionary<string, ProductJsonDAO> data)
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

        private static void InsertProduct(ShopMateContext db, Store vendor, KeyValuePair<string, ProductJsonDAO> entry)
        {
            var barcode = Gtin14.FromStandardBarcode(entry.Key);
            var dao = entry.Value;

            if (!(db.Set<Product>().Find(barcode) is null))
            {
                return;
            }

            var product = new Product(
                barcode,
                dao.Name.LimitLength(50),
                dao.Weight,
                dao.Volume,
                dao.Units,
                dao.OriginCountry?.LimitLength(2),
                dao.Edible,
                dao.Price,
                dao.Pictures,
                dao.AvailableStock,
                dao.TimesSold);
            product.Vendors.Add(vendor);

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
