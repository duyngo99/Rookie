using System.Collections.Generic;

namespace BackEnd.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartID { get; set; }
        public List<ShoppingCartItem>  ShoppingCartItems { get; set; }
    }
}