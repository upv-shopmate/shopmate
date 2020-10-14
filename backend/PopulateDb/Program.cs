using ShopMate.Models;

namespace PopulateDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ShopMateContext();

            db.Set<Store>().Add(new Store("Test", "EUR"));
            db.SaveChanges();
        }
    }
}
