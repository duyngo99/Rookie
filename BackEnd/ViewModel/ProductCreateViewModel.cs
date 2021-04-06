using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BackEnd.ViewModel
{
    public class ProductCreateViewModel
    {
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ProductImage  { get; set ;}
        public int CategoryID { get; set; }
    }
}