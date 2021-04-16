using Microsoft.AspNetCore.Mvc;
using CustomerSite.Helpers;
using System.Collections.Generic;
using CustomerSite.Models;
using System.Linq;

namespace CustomerSite.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            var cart= SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            ViewBag.cart=cart;
            ViewBag.total=cart.Sum(items=>items.Product.Price*items.Quantity);
            return View();
        }
        public IActionResult Pay()
        {
            var cart= SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            cart.Clear();
            ViewBag.cart=cart;
            ViewBag.total=0;
            SessionHelper.SetObjectAsJson(HttpContext.Session,"cart",cart);
            return RedirectToAction("Index","Home");
        }
    }
}