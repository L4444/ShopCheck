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

        public IList<Product> ReadAllProducts()
        {
            DbSet<Product> allProducts = _db.Products;
            //allProducts.Add(new ShopItem { Name = "Rand" });
            return allProducts.ToArray();

        }

        public List<ValidationResult> CreateNewProduct(Product product)
        {
            

            ValidationContext validationContext = new ValidationContext(product);
            List<ValidationResult> results = new List<ValidationResult>();

            if(!Validator.TryValidateObject(product, validationContext, results, true))
            {
                return results;
            }

            
            _db.Products.Add(product);
            _db.SaveChanges();
            return null;
        }


    }
}
