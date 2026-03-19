using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckDb
{
    public interface IShopCheckService
    {
        

        public IList<Product> ReadAllProducts();

        public Product? ReadProduct(int id);
        public ServiceResult DeleteProduct(int id);

        public ServiceResult UpdateProduct(int id, Product product);

        public ServiceResult CreateProduct(Product product);
    }
}
