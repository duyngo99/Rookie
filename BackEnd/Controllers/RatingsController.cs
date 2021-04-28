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
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        public RatingsController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RatingVm>>> GetRating()
        {
            return await _dataContext.Rating.Select(x => new RatingVm
            {
                Id = x.RatingID,
                RatingText = x.RatingText,
                ProductID = x.ProductID,
                UserName = x.UserName

            }).ToListAsync();

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<RatingVm>> PostRating(RatingVm x)
        {
            var rating = new Rating
            {

                RatingText = x.RatingText,
                ProductID = x.ProductID,
                UserName = x.UserName
            };
            _dataContext.Rating.Add(rating);
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingVm>> GetRatingById(int id)
        {

            var rating = await _dataContext.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            var product = new RatingVm
            {
                Id = rating.RatingID,
                UserName = rating.UserName,
                ProductID = rating.ProductID,
                RatingText = rating.RatingText


            };
            return product;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteRating(int id)
        {
            var rating = await _dataContext.Rating.FirstOrDefaultAsync(x => x.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }
            _dataContext.Rating.Remove(rating);
            await _dataContext.SaveChangesAsync();
            return NoContent();

        }





    }
}