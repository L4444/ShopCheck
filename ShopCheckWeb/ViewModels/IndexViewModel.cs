using ShopCheckDb;

namespace ShopCheckWeb.ViewModels
{
    public class IndexViewModel
    {
        public IList<Product> Products { get; set; }
        public Product NewProduct { get; set; }
    }
}
