namespace BackEnd.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string Username { get; set; }
        public decimal Total { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
    }
}