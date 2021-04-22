using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services.Interface;
using CustomerSite.Helpers;
using Microsoft.AspNetCore.Http;

namespace CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productClient;
        public CartController(IProductApiClient productClient)
        {
            _productClient = productClient;
        }
        public IActionResult Index(){
            var cart= SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            ViewBag.cart=cart;
            if(cart!=null)
            {
            ViewBag.total=cart.Sum(pro=>pro.Product.Price*pro.Quantity);
            }
            return View();
        }
        private int isExists(int id){
            List<Item> cart= SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            for(int i=0;i<cart.Count;i++)
            {
                if(cart[i].Product.ProductID == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public async Task<IActionResult> Buy(int id){
            if(SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart")==null)
            {
                List<Item> cart=new List<Item>();
                cart.Add(new Item{Product=await _productClient.GetProductById(id),Quantity=1});
                SessionHelper.SetObjectAsJson(HttpContext.Session,"cart",cart);
            }
            else{
                List<Item> cart=SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
                int index=isExists(id);
                if(index!=-1)
                {
                    cart[index].Quantity++;
                }
                else{
                    cart.Add(new Item{
                        Product=await _productClient.GetProductById(id),Quantity=1
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session,"cart",cart);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Remove(int id){
            List<Item> cart=SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            int index=isExists(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session,"cart",cart);
            return RedirectToAction("Index");
        }
         public IActionResult AddItem(int id){
            List<Item> cart=SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            int index=isExists(id);
            if(index!=-1)
            {
                cart[index].Quantity++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session,"cart",cart);
            return RedirectToAction("Index");
        }
         public IActionResult DropItem(int id){
            List<Item> cart=SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"cart");
            int index=isExists(id);
            if(index!=-1)
            {
                if(cart[index].Quantity!=0)
                    cart[index].Quantity--;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session,"cart",cart);
            return RedirectToAction("Index");
        }
    }
}
