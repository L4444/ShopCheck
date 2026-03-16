using Microsoft.EntityFrameworkCore;

namespace ShopCheckDb
{
    public class ShopCheckService
    {

        private ShopCheckDbContext _db = null;
        public string DbPath { get { return _db.DbPath; } }
        public bool IsDBCreated { get; }

        public ShopCheckService(ShopCheckDbContext db)
        {
            _db = db;
            IsDBCreated = _db.Database.EnsureCreated();


            // If it's created, seed it with data.
            if (IsDBCreated)
            {

                CreateNewShopItem(new ShopItem { Name = "John Item", Number =44,  });
                CreateNewShopItem(new ShopItem { Name = "Two Item", Number = 66,  });
            }
            

        }

        public IEnumerable<ShopItem> ReadAllShopItems()
        {

            return _db.ShopItems.ToArray();

        }

        public void CreateNewShopItem(ShopItem item)
        {
            item.DateCreated = DateTime.Now;
            _db.ShopItems.Add(item);
            _db.SaveChanges();
        }


    }
}
