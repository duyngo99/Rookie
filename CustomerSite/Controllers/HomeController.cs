using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services.Interface;
using Microsoft.AspNetCore.Http;
using Share;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;

        private readonly IRatingApiClient _ratingApiClient;

        public HomeController(ILogger<HomeController> logger,IProductApiClient productApiClient,IRatingApiClient ratingApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _ratingApiClient = ratingApiClient;          
        }
        
        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetProducts();
            return View(products);  
        }

        public async Task<IActionResult> Detail(int id){    
            ProductFormVm productFormVm = new ProductFormVm();
            await _productApiClient.PuttRatingProduct(id,productFormVm);
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
        
        public async Task<IActionResult> Rating(IFormCollection form) {
            int proId=int.Parse(form["proId"]);
            string userName=form["userName"].ToString();
            double rate=double.Parse(form["rate"]);
            if(await _ratingApiClient.SearchRating(proId,userName ) != null) {
                  _ratingApiClient.RemoveRating(proId,userName);
            }                          
            await _ratingApiClient.PostRatingByID(proId,userName,rate);
            
            return RedirectToAction("Index","Home");
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
