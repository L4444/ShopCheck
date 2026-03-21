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
            
            return allProducts.ToList();

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
            Product? productToDelete = ReadProduct(id);
            ServiceResult sr = new ServiceResult();

            if(productToDelete == null)
            {
                sr.ValidationErrors.Add("DELETE", "Cannot find " + id);
            }

            if(sr.IsValid)
            {
                _db.Products.Remove(productToDelete);
                _db.SaveChanges();
            }
            return sr; 

        }

        public ServiceResult UpdateProduct(Product product)
        {
            var sr = Validate(product);



            if (sr.IsValid)
            {
                Product? existing = ReadProduct(product.Id);

                if(existing == null)
                {
                    sr.ValidationErrors.Add("NULL", $"Cannot read product {product.Id}");
                    return sr;
                }

                existing.Name = product.Name;
                existing.Url = product.Url;
                existing.MinStock = product.MinStock;
                _db.SaveChanges();
            }
            return sr;
        }

        private ServiceResult Validate(Product product)
        {
            ValidationContext validationContext = new ValidationContext(product);
            List<ValidationResult> results = new List<ValidationResult>();
            ServiceResult sr = new ServiceResult();
            if (!Validator.TryValidateObject(product, validationContext, results, true))
            {
                int i = 0;
                foreach (var r in results)
                {
                    sr.ValidationErrors.Add(i.ToString(), r.ErrorMessage);
                    i++;
                }
                return sr;
            }

            return sr;

        }

        public ServiceResult CreateProduct(Product product)
        {



            var sr = Validate(product);


            if (sr.IsValid)
            {
                product.Id = 0;
                _db.Products.Add(product);
                _db.SaveChanges();
            }
            return sr;
        }


    }
}
