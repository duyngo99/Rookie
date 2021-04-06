using System;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductManagerController(ApplicationDbContext applicationDbContext, IHostingEnvironment hostingEnvironment )
        {
            _applicationDbContext  = applicationDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Create (){
            return View(); 
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductCreateViewModel model){
            if(ModelState.IsValid) {
                byte [] p1 = null;
                if(model.ProductImage !=null){
                    using(var fs1 = model.ProductImage.OpenReadStream())
                    using(var ms1 = new MemoryStream()){
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                Product product = new Product {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Description = model.Description,
                    ProductImage = p1,
                    CategoryID = model.CategoryID

                };
                _applicationDbContext.Products.Add(product);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View();
        } 
    }
}