using Microsoft.AspNetCore.Http;

namespace Share
{
    public class ProductVm
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Image { get; set; }
        public double RatingAVG { get; set; }
        public double Quantity { get; set; }
    }
}
