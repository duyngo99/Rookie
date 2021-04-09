namespace Share
{
    public class ProductFormVm
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public byte[] Image { get; set; }
        public double RatingAVG  { get ; set ;}
    }
}