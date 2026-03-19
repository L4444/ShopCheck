using Microsoft.AspNetCore.Mvc;
using ShopCheckDb;
using ShopCheckWeb.Models;
using ShopCheckWeb.ViewModels;
using System.Diagnostics;

namespace ShopCheckWeb.Controllers
{
    public class HomeController : Controller
    {
        private IShopCheckService _service = default!;

      
        public HomeController(IShopCheckService service)
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

            var results = _service.CreateProduct(newProduct);
            if (results.IsValid)
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
