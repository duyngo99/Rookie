namespace BackEnd.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemID { get; set; }
        public int Amount { get; set; }
        public Product Product { get ; set ;}
        public string ShoppingCartID { get; set; }
    }
}