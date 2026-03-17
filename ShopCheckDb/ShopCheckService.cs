using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

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
        
            

        }

        public IList<ShopItem> ReadAllShopItems()
        {
            DbSet<ShopItem> allProducts = _db.ShopItems;
            //allProducts.Add(new ShopItem { Name = "Rand" });
            return allProducts.ToArray();

        }

        public List<ValidationResult> CreateNewShopItem(ShopItem item)
        {
            item.DateCreated = DateTime.Now;

            ValidationContext validationContext = new ValidationContext(item);
            List<ValidationResult> results = new List<ValidationResult>();

            if(!Validator.TryValidateObject(item, validationContext, results, true))
            {
                return results;
            }

            
            _db.ShopItems.Add(item);
            _db.SaveChanges();
            return null;
        }


    }
}
