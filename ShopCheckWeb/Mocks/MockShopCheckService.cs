using ShopCheckDb;

namespace ShopCheckWeb.Mocks
{
    public class MockShopCheckService : IShopCheckService
    {


        public ServiceResult CreateProduct(Product product)
        {
            Console.WriteLine($"Create New Called");
            Console.WriteLine($"Name: \'{product.Name}\'");
            Console.WriteLine($"Url: \'{product.Url}\'");
            Console.WriteLine($"Min Stock: \'{product.MinStock}\'");

            ServiceResult sr = new ServiceResult();
            sr.ValidationErrors.Add("KEY", "VALUE");

            return sr;
        }

        public ServiceResult DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Product> ReadAllProducts()
        {
            List<Product> dummyData = new List<Product>();

            dummyData.Add(new Product { MinStock = 1, Name = "Bacon1", Url = "www.Bacon1.com" });
            dummyData.Add(new Product { MinStock = 2, Name = "Bacon2", Url = "www.Bacon2.com" });
            dummyData.Add(new Product { MinStock = 3, Name = "Bacon3", Url = "www.Bacon3.com" });

            return dummyData;
        }

        public Product? ReadProduct(int id)
        {
            Console.WriteLine("Read Product Called");
            return null;
        }

        public ServiceResult UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
