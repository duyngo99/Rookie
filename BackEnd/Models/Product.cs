

namespace BackEnd.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public double RatingAverage { get; set; }


    }

}