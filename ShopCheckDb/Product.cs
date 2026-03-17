using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopCheckDb
{
    public class Product
    {
        public int Id { get; private set; }
        

        
        [MaxLength(10)]
        public string Name { get; set; }

        public string Url { get; set; }

        [Range(0,10)]
        public int MinStock { get; set; }

      

  

    }

    public enum SuperMarket
    {
        LIDL = 1,
        SAINSBURYS = 2,
        TESCO = 4
    }
}
