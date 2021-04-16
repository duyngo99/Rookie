namespace Share
{
    public class ShoppingCartItemVm
    {
        public int ShoppingCartItemID { get; set; }
        public int Amount { get; set; }
        public ProductVm Product { get ; set ;}
        public string ShoppingCartID { get; set; }

        
    }
}