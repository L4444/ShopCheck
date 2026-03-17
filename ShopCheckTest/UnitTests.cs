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
        public void RunTest(IList<Product> inputProducts, IList<Product> expectedProducts)
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            try
            {

                con.Database.EnsureCreated();
                ShopCheckService serve = new ShopCheckService(con);
            }
            finally
            {
                con.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void TestRunTest()
        {
            IList<Product> inputItems = [
                   new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
                    new Product { Name = "Eggs2", MinStock = 6,  Url = "www.eggs2.com" },
              new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            RunTest(inputItems, inputItems);
        }

       [TestMethod]
        public void TestUpdateProduct()
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            try
            {

                con.Database.EnsureCreated();
                ShopCheckService serve = new ShopCheckService(con);

                IList<Product> inputItems = [
                  new Product { Name = "Eggs1", MinStock = 3, Url = "www.eggs1.com" },
                new Product { Name = "Eggs2", MinStock = 3, Url = "www.eggs2.com" },
                new Product { Name = "Eggs3", MinStock = 3, Url = "www.eggs3.com" }];


                
                IList<Product> expectedProducts = [
                  new Product { Name = "Eggs1-Altered", MinStock = 4, Url = "www.eggs1-altered.com" },
                new Product { Name = "Eggs2", MinStock = 3, Url = "www.eggs2.com" },
                new Product { Name = "Eggs3", MinStock = 3, Url = "www.eggs3.com" }];


                List<ValidationResult> result = null!;
                foreach (Product si in inputItems)
                {
                    serve.CreateProduct(si);

                }
                serve.UpdateProduct(1, expectedProducts[0]);
                IList<Product> actualProducts = serve.ReadAllProducts();
                 
                actualProducts.Should().BeEquivalentTo(expectedProducts, options => options.Excluding(p => p.Id));
            }
            finally
            {
                con.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void TestReadProduct()
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            try
            {

                con.Database.EnsureCreated();
                ShopCheckService serve = new ShopCheckService(con);

                IList<Product> inputItems = [
                  new Product { Name = "Eggs1", MinStock = 3, Url = "www.eggs1.com" },
                new Product { Name = "Eggs2", MinStock = 3, Url = "www.eggs2.com" },
                new Product { Name = "Eggs3", MinStock = 3, Url = "www.eggs3.com" }];


                // We expect only 
                Product expectedProduct = inputItems[0];

                List<ValidationResult> result = null!;
                foreach (Product si in inputItems)
                {
                    serve.CreateProduct(si);

                }

                // SQLLite starts at index 1?
                Product actualProduct = serve.ReadProduct(1);
                actualProduct.Should().BeEquivalentTo(inputItems[0]);
            }
            finally
            {
                con.Database.EnsureDeleted();
            }

        }


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

                List<ValidationResult> result = null!;
                foreach (Product si in inputItems)
                {
                   

                    result = serve.CreateProduct(si);
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
                    
                    
                    serve.CreateProduct(si);
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
