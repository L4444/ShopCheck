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
        private ShopCheckDbContext con;
        private ShopCheckService serve;
        [TestInitialize]
        public void Setup()
        {
            con = new ShopCheckDbContext();

            con.Database.EnsureCreated();
            serve = new ShopCheckService(con);

        }

        [TestCleanup]
        public void Cleanup()
        {
            con.Database.EnsureDeleted();
        }

     

    

       [TestMethod]
        public void TestUpdateProduct()
        {
         

            IList<Product> inputItems = [
                new Product { Name = "Eggs1", MinStock = 3, Url = "www.eggs1.com" },
            new Product { Name = "Eggs2", MinStock = 3, Url = "www.eggs2.com" },
            new Product { Name = "Eggs3", MinStock = 3, Url = "www.eggs3.com" }];


                
            IList<Product> expectedProducts = [
                new Product { Name = "Eggs1-Altered", MinStock = 4, Url = "www.eggs1-altered.com" },
            new Product { Name = "Eggs2", MinStock = 3, Url = "www.eggs2.com" },
            new Product { Name = "Eggs3", MinStock = 3, Url = "www.eggs3.com" }];


            List<ValidationResult> result = null!;
            foreach (Product p in inputItems)
            {
                serve.CreateProduct(p);

            }
            serve.UpdateProduct(1, expectedProducts[0]);
            IList<Product> actualProducts = serve.ReadAllProducts();
                 
            actualProducts.Should().BeEquivalentTo(expectedProducts, options => options.Excluding(p => p.Id));
          
        }

        [TestMethod]
        public void TestReadProduct()
        {
            // Arrange
            IList<Product> inputItems = [
            new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
            new Product { Name = "Eggs2", MinStock = 6,  Url = "www.eggs2.com" },
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            Product expectedProduct = 
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" };


            // Act
            foreach (Product p in inputItems)
            {
                serve.CreateProduct(p);
            }

            // -- Don't forget, SQLLite starts indexing at 1 not 0
            Product actualItem = serve.ReadProduct(3);


            // Assert
            actualItem.Should().BeEquivalentTo(expectedProduct, options => options.Excluding(si => si.Id));
        }


        [TestMethod]
        public void TestBadItems()
        {
            // Arrange
            IList<Product> inputItems = [
            new Product { Name = "Eggs1_TOO_LONG_NAME", MinStock = 3,Url = "www.eggs1-TOO_LONG_NAME.com" },
            new Product { Name = "Eggs2", MinStock = -1,  Url = "www.eggs2-NEGATIVE_MIN_STOCK.com" },
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            IList<Product> expectedItems = [
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            // Act
            foreach (Product si in inputItems)
            {
                serve.CreateProduct(si);
            }

            IList<Product> actualItems = serve.ReadAllProducts();


            // Assert
            actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id));
        }

        [TestMethod]
        public void TestValidItems()
        {
         
            // Arrange
            IList<Product> inputItems = [
            new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
            new Product { Name = "Eggs2", MinStock = 6,  Url = "www.eggs2.com" },
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];

            
            IList<Product> expectedItems = [
            new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
            new Product { Name = "Eggs2", MinStock = 6,  Url = "www.eggs2.com" },
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            // Act
            foreach (Product si in inputItems)
            {
                serve.CreateProduct(si);
            }

            IList<Product> actualItems = serve.ReadAllProducts();


            // Assert
            actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id));


          
            
            
        }

        [TestMethod]
        public void TestDeleteProduct()
        {

            // Arrange
            IList<Product> inputItems = [
            new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
            new Product { Name = "Eggs2", MinStock = 6,  Url = "www.eggs2.com" },
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            IList<Product> expectedItems = [
            new Product { Name = "Eggs1", MinStock = 3,Url = "www.eggs1.com" },
            new Product { Name = "Eggs3", MinStock = 9,  Url = "www.eggs3.com" }];


            // Act
            foreach (Product si in inputItems)
            {
                serve.CreateProduct(si);
            }

            serve.DeleteProduct(2);

            IList<Product> actualItems = serve.ReadAllProducts();


            // Assert
            actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id));





        }
    }
}
