using Microsoft.EntityFrameworkCore;
using ShopCheckDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckTest
{
    internal static class Utility
    {

        internal static ShopCheckDbContext GetShopCheckDbContext()
        {
            // Put the database file in the project directory
            var cd = Path.Join(Environment.CurrentDirectory, "..", "..", "..", "Shopping.db");

            string path = Path.GetFullPath(cd);
            Console.WriteLine($"Getting ShopDbContext from \'{path}\'");

            var options = new DbContextOptionsBuilder<ShopCheckDbContext>()
                .UseSqlite($"Data Source={path}")
                .Options;

            var con = new ShopCheckDbContext(options);
            
            return con;
        }
    }
}
