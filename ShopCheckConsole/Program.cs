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



bool isRunning = true;
while(isRunning)
{
    Console.WriteLine("Welcome to my crude testing framework");
    Console.WriteLine("q: quit");
    Console.WriteLine("a: add dummy entry");
    Console.WriteLine("l: list all entries");

    Console.WriteLine("Enter a command: ");
    char key = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if(key == 'q')
    {
        isRunning = false;
    }

    if (key == 'l')
    {
        OutputEntries();
    }

    if(key == 'a')
    {
        shopCheckService.CreateNewShopItem(new ShopItem { Name = "Evil" });
    }

}




void OutputEntries()
{
    foreach (ShopItem s in shopCheckService.ReadAllShopItems())
    {
        Console.WriteLine($"Name: {s.Name} Number: {s.Number} Date Created: {s.DateCreated.ToString()}");

    }
}






