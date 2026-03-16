using ShopCheckDb;
using FluentAssertions;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace ShopCheckTest
{
    [TestClass]
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
                Assert.IsEmpty(serve.ReadAllShopItems());

                IList<ShopItem> expectedItems = [
                    new ShopItem { Name = "Eggs1ffffffffffffffffffffffffffffffffffff", MinStock = -1, MaxStock = 12, Url = "www.eggs1.com" },
                 new ShopItem { Name = "Eggs2fffffffffffffffffffffffffffffff", MinStock = 6, MaxStock = 12, Url = "www.eggs2.com" },
                new ShopItem { Name = "Eggs444444", MinStock = 9, MaxStock = 12, Url = "www.eggs3.com" }];


                List<ValidationResult> result = null;
                foreach (ShopItem si in expectedItems)
                {
                    // Quick and dirty way to get a deep copy
                    //ShopItem copy = JsonSerializer.Deserialize<ShopItem>(JsonSerializer.Serialize(si));

                    result = serve.CreateNewShopItem(si);
                }
                Console.WriteLine(result);

                IList<ShopItem> actualItems = serve.ReadAllShopItems();

                actualItems.Should().BeEmpty();


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
                Assert.IsEmpty(serve.ReadAllShopItems());

                IList<ShopItem> expectedItems = [
                    new ShopItem { Name = "Eggs1", MinStock = 3, MaxStock = 12, Url = "www.eggs1.com" },
                    new ShopItem { Name = "Eggs2", MinStock = 6, MaxStock = 12, Url = "www.eggs2.com" },
              new ShopItem { Name = "Eggs3", MinStock = 9, MaxStock = 12, Url = "www.eggs3.com" }];



                foreach(ShopItem si in expectedItems)
                {
                    // Quick and dirty way to get a deep copy
                    ShopItem copy = JsonSerializer.Deserialize<ShopItem>(JsonSerializer.Serialize(si));
                    
                    serve.CreateNewShopItem(copy);
                }

                IList<ShopItem> actualItems = serve.ReadAllShopItems();  

                expectedItems.Should().BeEquivalentTo(actualItems, options => options.Excluding(si => si.Id).Excluding(si => si.DateCreated));


            }
            finally
            {
                con.Database.EnsureDeleted();
            }
            
            
        }
    }
}
