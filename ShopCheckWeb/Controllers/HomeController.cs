using Microsoft.AspNetCore.Mvc;
using ShopCheckDb;
using ShopCheckWeb.Models;
using ShopCheckWeb.ViewModels;
using System.Diagnostics;

namespace ShopCheckWeb.Controllers
{
    public class HomeController : Controller
    {
        private ShopCheckService _service = default!;

      
        public HomeController(ShopCheckService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            vm.Products = _service.ReadAllProducts();
            vm.NewProduct = new Product();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            Console.WriteLine($"Create New Called");
            Console.WriteLine($"Name: \'{newProduct.Name}\'");
            Console.WriteLine($"Url: \'{newProduct.Url}\'");
            Console.WriteLine($"Min Stock: \'{newProduct.MinStock}\'");


            var results = _service.CreateProduct(newProduct);
            if (results == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var vm = new IndexViewModel();
                vm.NewProduct = newProduct;
                vm.Products = _service.ReadAllProducts();
                return View("Index", vm);
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
