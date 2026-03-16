using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShopCheckDb
{
    public class ShopCheckDbContext : DbContext
    {
        public DbSet<ShopItem> ShopItems { get; set; }

        public string DbPath { get; }
        public ShopCheckDbContext()
        {
            // Put the database file in the project directory
            var cd = Path.Join(Environment.CurrentDirectory, "..", "..", "..", "..", "Shopping.db");

            DbPath = Path.GetFullPath(cd);

        }

  

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");
    }
}
