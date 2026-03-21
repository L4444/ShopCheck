using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShopCheckWeb;
using ShopCheckWeb.Models;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;

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
            var products = _service.ReadAllProducts();   
            return View(products);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        
        public IActionResult Create()
        {
            var product = new Product();
            product.MinStock = 1;
            return View("ProductForm",product);
        }

        public IActionResult Edit(int id)
        {
            var product = _service.ReadProduct(id);   
            return View("ProductForm", product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var results = _service.UpdateProduct(product);
            return Validate(results, product);
        }

        private IActionResult Validate(ServiceResult serviceResult, Product product)
        {
            if (serviceResult.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.Clear();

                foreach (var kvp in serviceResult.ValidationErrors)
                {
                    ModelState.AddModelError(kvp.Key, kvp.Value);
                }

                return View("ProductForm", product);
            }
        }
        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var results = _service.CreateProduct(product);
            return Validate(results, product);
            
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
