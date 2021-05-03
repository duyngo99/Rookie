using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Share;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        public OrdersController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrder()
        {
            return await _dataContext.Orders.Select(x => new OrderVm
            {
                OrderID = x.OrderID,
                Total = x.Total,
                Amount = x.Amount,
                Username = x.Username,
                Status = x.Status
            }).ToListAsync();

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> PostOrder(OrderVm x)
        {
            var order = new Order
            {
                Total = x.Total,
                Amount = x.Amount,
                Username = x.Username,
                Status = x.Status
            };
            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingVm>> GetOrderById(int id)
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
        public async Task<ActionResult> DeleteOrder(int id)
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

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateCategory(int id, OrderFormVm model)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            order.Amount = model.Amount;
            order.Status = model.Status;
            order.Total = model.Total;
            order.Username = model.Username;
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }


    }
}