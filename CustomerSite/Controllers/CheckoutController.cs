using Microsoft.AspNetCore.Mvc;
using CustomerSite.Helpers;
using System.Collections.Generic;
using CustomerSite.Models;
using System.Linq;
using Share;
using CustomerSite.Services.Interface;

namespace CustomerSite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOrderApiClient _orderClient;
        public CheckoutController(IOrderApiClient orderApiClient)
        {
            _orderClient = orderApiClient;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(items => items.Product.Price * items.Quantity);
            return View();
        }
        public IActionResult Pay()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            int count = 0;
            foreach (var item in cart)
            {
                count++;
            }

            OrderFormVm orderFormVm = new OrderFormVm
            {
                Amount = count,
                Total = ViewBag.total,
                Username = User.Identity.Name
            };
            _orderClient.PostOrders(orderFormVm);
            cart.Clear();
            ViewBag.cart = cart;
            ViewBag.total = 0;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Home");
        }


    }
}