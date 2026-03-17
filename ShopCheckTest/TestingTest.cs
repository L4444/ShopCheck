using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckTest
{
    [TestClass]
    public class TestingTest
    {
        public static string FakeDB { get; set; }
        [TestMethod]
        public void TestFirst()
        {
            FakeDB += "Worst";
            Console.WriteLine($"About to open fake DB{FakeDB}");
            FakeDB += "First";
            
        }

        [TestMethod]
        public void TestSecond()
        {
            Console.WriteLine($"About to open fake DB{FakeDB}");
            FakeDB += "Second";

        }
    }
}
