using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopCheckDb
{
    public class ShopItem
    {
        public int Id { get; private set; }
        public DateTime DateCreated { get; internal set; }

        
        [MaxLength(5)]
        public string Name { get; set; }

        [Column(TypeName="varchar(20)")]
        public string? Url { get; set; }

        public int? MaxStock { get; set; }
        public int? MinStock { get; set; }


        public int? CurrentStock { get; set; }

        public SuperMarket? BuyFrom { get; set; }

  

    }

    public enum SuperMarket
    {
        LIDL = 1,
        SAINSBURYS = 2,
        TESCO = 4
    }
}
