using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share;

namespace BackEnd.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class RatingsController:ControllerBase
    {
         private readonly ApplicationDbContext _dataContext;
        public RatingsController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RatingVm>>> GetRating(){
            return await  _dataContext.Rating.Select(x => new RatingVm{
                Id=x.RatingID,
               RatingText = x.RatingText,

            ProductID = x.ProductID,
            UserName = x.UserName
                
            }).ToListAsync();
            
        }
         [HttpPost]
        public async Task<ActionResult<RatingVm>> PostRating(RatingVm x){
            var product = new Rating {
                
               RatingText = x.RatingText,
            ProductID = x.ProductID,
            UserName = x.UserName
            };
            _dataContext.Rating.Add(product);
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingVm>> GetRatingById(int id ){
            var products = await _dataContext.Rating.FindAsync(id);
            if (products == null){
                return NotFound();
            }
            var product = new RatingVm {
                Id =products.RatingID,
                UserName = products.UserName,
                ProductID = products.ProductID,
                RatingText = products.RatingText

                
            };
            return product;
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteRating(int id){
            var product = await _dataContext.Rating.FirstOrDefaultAsync(x => x.RatingID ==id);
            if(product == null){
                return NotFound();
            }
            _dataContext.Rating.Remove(product);
            await _dataContext.SaveChangesAsync();
            return NoContent();
            
        }
    }
}