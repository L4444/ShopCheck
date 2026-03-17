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
                Assert.IsEmpty(serve.ReadAllShopItems());

                IList<ShopItem> inputItems = [
                    new ShopItem { Name = "Eggs1_Too_Long_Name", MinStock = -1, MaxStock = 12, Url = "www.eggs1.com" },
                 new ShopItem { Name = "Eggs2_Too_Long_Name", MinStock = 6, MaxStock = 12, Url = "www.eggs2.com" },
                new ShopItem { Name = "Eggs3", MinStock = 9, MaxStock = 12, Url = "www.eggs3.com" }];

                // We expect only the 3rd item to be stored (eggs3)
                List<ShopItem> expectedItems = new List<ShopItem>();
                expectedItems.Add(inputItems[2]);

                List<ValidationResult> result = null;
                foreach (ShopItem si in inputItems)
                {
                   

                    result = serve.CreateNewShopItem(si);
                }
                Console.WriteLine(result);

                IList<ShopItem> actualItems = serve.ReadAllShopItems();

                actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id).Excluding(si => si.DateCreated));


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

                IList<ShopItem> inputItems = [
                    new ShopItem { Name = "Eggs1", MinStock = 3, MaxStock = 12, Url = "www.eggs1.com" },
                    new ShopItem { Name = "Eggs2", MinStock = 6, MaxStock = 12, Url = "www.eggs2.com" },
              new ShopItem { Name = "Eggs3", MinStock = 9, MaxStock = 12, Url = "www.eggs3.com" }];

                // We expect all items to be stored
                IList<ShopItem> expectedItems = inputItems;

                foreach (ShopItem si in inputItems)
                {
                    
                    
                    serve.CreateNewShopItem(si);
                }

                IList<ShopItem> actualItems = serve.ReadAllShopItems();

                actualItems.Should().BeEquivalentTo(expectedItems, options => options.Excluding(si => si.Id).Excluding(si => si.DateCreated));


            }
            finally
            {
                con.Database.EnsureDeleted();
            }
            
            
        }
    }
}
