using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShopCheckWeb
{
    public class ShopCheckDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ShopCheckDbContext(DbContextOptions<ShopCheckDbContext> options)
       : base(options)
        {
        }
       

  

    
    }
}
