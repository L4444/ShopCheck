using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopCheckWeb
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(10, ErrorMessage = "The maximum length for a product name is 10 letters")]
        [Required(ErrorMessage = "A product name is required")]
        public string Name { get; set; } = String.Empty;

        public string? Url { get; set; }

        [Range(1,10)]
        public int MinStock { get; set; }

    }

    public enum SuperMarket
    {
        LIDL = 1,
        SAINSBURYS = 2,
        TESCO = 4
    }
}
