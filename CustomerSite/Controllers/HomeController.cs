using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services;
using Microsoft.AspNetCore.Http;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger,IProductApiClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetProducts();
            return View(products);  
        }

        public async Task<IActionResult> Detail(int id){
            var products = await _productApiClient.GetProductById(id);
            return View(products);

        }

        public async Task<IActionResult> CategoryList(int id){
            var products = await _productApiClient.GetProductByCate(id);
            return View(products);
        }
        public async Task<IActionResult> Search(IFormCollection form){
            var products = await _productApiClient.GetProductByName(form["name"].ToString());
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
