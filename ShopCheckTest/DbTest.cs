using Microsoft.EntityFrameworkCore;
using ShopCheckWeb;
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
            var con = Utility.GetShopCheckDbContext();
            con.Database.EnsureCreated();
        }

        [TestMethod]
        public void TestDestroy()
        {
            var con = Utility.GetShopCheckDbContext();
            con.Database.EnsureDeleted();
        }
    }
}
