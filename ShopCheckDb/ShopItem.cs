using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckDb
{
    public class ShopItem
    {
        public int Id { get; private set; }
        public DateTime DateCreated { get; internal set; }
        public string Name { get; set; }

        public int Number { get; set; }
        

        
    }
}
