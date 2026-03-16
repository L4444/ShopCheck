using ShopCheckDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckTest
{
    [TestClass]
    public class DbTest
    {
        [TestMethod]
        public void TestCreate()
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            con.Database.EnsureCreated();
        }

        [TestMethod]
        public void TestDestroy()
        {
            ShopCheckDbContext con = new ShopCheckDbContext();
            con.Database.EnsureDeleted();
        }
    }
}
