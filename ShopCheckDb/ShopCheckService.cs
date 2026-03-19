using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace ShopCheckDb
{
    public class ShopCheckService : IShopCheckService
    {

        private ShopCheckDbContext _db = null!;
        
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

        /// <summary>
        /// Tries to return a produce with the id specified, if it can't returns null
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>  returns null if cannot find </returns>
        public Product? ReadProduct(int id)
        {
            Product? product = _db.Products.Find(id);
            
            return product;
        }

        public ServiceResult DeleteProduct(int id)
        {
            Product productToDelete = ReadProduct(id);
            _db.Products.Remove(productToDelete);
            _db.SaveChanges();
            return null; 

        }

        public ServiceResult UpdateProduct(int id, Product product)
        {
            Product existing = ReadProduct(id);
            existing.Name = product.Name;
            existing.Url = product.Url;
            existing.MinStock = product.MinStock;
            _db.SaveChanges();
            return null;
        }

        public ServiceResult CreateProduct(Product product)
        {
            

            ValidationContext validationContext = new ValidationContext(product);
            List<ValidationResult> results = new List<ValidationResult>();

            if(!Validator.TryValidateObject(product, validationContext, results, true))
            {
                return null;
            }

            
            _db.Products.Add(product);
            _db.SaveChanges();
            return null;
        }


    }
}
