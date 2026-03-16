using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShopCheckDb;


// Simulate Dependency Injection
ShopCheckDbContext con = new ShopCheckDbContext();
ShopCheckService shopCheckService = new ShopCheckService(con);



string path = $"Db Path is: \'{shopCheckService.DbPath}\'";

string startMessage = "";
startMessage = shopCheckService.IsDBCreated ? "Creating database..." : "Database Exists ";


Console.WriteLine(startMessage + path);

shopCheckService.CreateNewShopItem(new ShopItem { Name = "Evil" });


foreach (ShopItem s in shopCheckService.ReadAllShopItems())
{
    Console.WriteLine($"Name: {s.Name} Number: {s.Number} Date Created: {s.DateCreated.ToString()}");

}







