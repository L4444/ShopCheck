using Microsoft.AspNetCore.Components.Forms;
using ShopCheckDb;

namespace ShopCheckWeb.Mocks
{
    public class MockShopCheckService : IShopCheckService
    {


        public ServiceResult CreateProduct(Product product)
        {
            Console.WriteLine($"CreateProduct() Called");



            ServiceResult sr = new ServiceResult();

            if (product.Name != "Valid")
            {

                sr.ValidationErrors.Add("Key 1", "Validation error 1");
                sr.ValidationErrors.Add("Key 2", "Validation error 2");
                sr.ValidationErrors.Add("Key 3", "Validation error 3");
                Console.WriteLine($"----- Validation failed");
            }
            else
            {
                Console.WriteLine($"----- Created new entry at Id: \'{product.Id}\'");
             
            }
            Console.WriteLine($"Name: \'{product.Name}\'");
            Console.WriteLine($"Url: \'{product.Url}\'");
            Console.WriteLine($"Min Stock: \'{product.MinStock}\'");
            return sr;
        }

        public ServiceResult DeleteProduct(int id)
        {
            Console.WriteLine($"Deleting Product with id \'{id}\'");
            return null;
        }

        public IList<Product> ReadAllProducts()
        {
            List<Product> dummyData = new List<Product>();

            dummyData.Add(new Product {  Id = 1, MinStock = 1, Name = "Bacon1", Url = "www.Bacon1.com" });
            dummyData.Add(new Product { Id = 2, MinStock = 2, Name = "Bacon2", Url = "www.Bacon2.com" });
            dummyData.Add(new Product { Id = 3, MinStock = 3, Name = "Bacon3", Url = "www.Bacon3.com" });
            
            return dummyData;
        }

        public Product? ReadProduct(int id)
        {
            
            return new Product { Id = id, MinStock = id, Name = $"EditName{id}", Url = $"www.EditUrl{id}.com" };
        }

        public ServiceResult UpdateProduct( Product product)
        {
            Console.WriteLine("UpdateProduct() Called");

            ServiceResult sr = new ServiceResult();
            if (product.Name != "Valid")
            {

                sr.ValidationErrors.Add("Key 1", "Validation error 1");
                sr.ValidationErrors.Add("Key 2", "Validation error 2");
                sr.ValidationErrors.Add("Key 3", "Validation error 3");
                Console.WriteLine($"----- Validation failed");
            }
            else
            {
                Console.WriteLine($"----- Edited entry \'{product.Id}\'");
             
            }
            Console.WriteLine($"Name: \'{product.Name}\'");
            Console.WriteLine($"Url: \'{product.Url}\'");
            Console.WriteLine($"Min Stock: \'{product.MinStock}\'");
            return sr;
        }
    }
}
