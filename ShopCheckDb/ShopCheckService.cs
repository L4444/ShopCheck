using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace ShopCheckDb
{
    public class ShopCheckService
    {

        private ShopCheckDbContext _db = null!;
        public string DbPath { get { return _db.DbPath; } }
        public bool IsDBCreated { get; }

        public ShopCheckService(ShopCheckDbContext db)
        {
            _db = db;
        
            

        }

        public IList<Product> ReadAllProducts()
        {
            DbSet<Product> allProducts = _db.Products;
            
            return allProducts.ToArray();

        }

        public Product ReadProduct(int id)
        {
            Product product = _db.Products.Find(id);
            
            return product;
        }

        public void DeleteProduct(int id)
        {
            Product productToDelete = ReadProduct(id);
            _db.Products.Remove(productToDelete);
            _db.SaveChanges();

        }

        public void UpdateProduct(int id, Product product)
        {
            Product existing = ReadProduct(id);
            existing.Name = product.Name;
            existing.Url = product.Url;
            existing.MinStock = product.MinStock;
            _db.SaveChanges();
        }

        public List<ValidationResult> CreateProduct(Product product)
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
