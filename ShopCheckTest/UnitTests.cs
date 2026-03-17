using ShopCheckDb;
using FluentAssertions;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace ShopCheckTest
{
    [TestClass]
    [DoNotParallelize]
    public sealed class UnitTests
    {

      


        [TestMethod]
        public void TestBadItems()
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            try
            {

                con.Database.EnsureCreated();


                ShopCheckService serve = new ShopCheckService(con);
                Assert.IsEmpty(serve.ReadAllProducts());

                IList<Product> inputItems = [
                    new Product { Name = "Eggs1_Too_Long_Name", MinStock = -1, Url = "www.eggs1-Too_Long_NameAndNegativeMinStock.com" },
                 new Product { Name = "Eggs2_Too_Long_Name", MinStock = 0, Url = "www.eggs2-TooLongName.com" },
                new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" },
                new Product { Name = "Eggs4", MinStock = -1,  Url = "www.eggs4-NegativeMinStock.com" }];

                // We expect only the 3rd item to be stored (eggs3)
                List<Product> expectedItems = new List<Product>();
                expectedItems.Add(inputItems[2]);

                List<ValidationResult> result = null;
                foreach (Product si in inputItems)
                {
                   

                    result = serve.CreateNewProduct(si);
                }
                Console.WriteLine(result);

                IList<Product> actualItems = serve.ReadAllProducts();

                actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id));


            }
            finally
            {
                con.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void TestValidItems()
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            try
            {
                
                con.Database.EnsureCreated();


                ShopCheckService serve = new ShopCheckService(con);
                Assert.IsEmpty(serve.ReadAllProducts());

                IList<Product> inputItems = [
                    new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
                    new Product { Name = "Eggs2", MinStock = 6,  Url = "www.eggs2.com" },
              new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];

                // We expect all items to be stored
                IList<Product> expectedItems = inputItems;

                foreach (Product si in inputItems)
                {
                    
                    
                    serve.CreateNewProduct(si);
                }

                IList<Product> actualItems = serve.ReadAllProducts();

                actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id));


            }
            finally
            {
                con.Database.EnsureDeleted();
            }
            
            
        }
    }
}
