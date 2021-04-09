namespace BackEnd.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public double RatingText { get; set; }
        public int ProductID  {get ; set;}
        public Product Product { get; set; }
        public string UserName { get; set; }
    }
}