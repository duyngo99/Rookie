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
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetCategory()
        {
            return await _dataContext.Orders.Select(x => new OrderVm
            {

            }).ToListAsync();


        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> GetCategory(int id)
        {
            var categories = await _dataContext.Orders.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            var category = new OrderVm
            {

            };
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<OrderVm>> CreateCategory([FromForm] OrderFormVm model)
        {
            var category = new Order
            {

            };
            _dataContext.Orders.Add(category);
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, OrderFormVm model)
        {
            var category = await _dataContext.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            if (category == null)
            {
                return NotFound();
            }

            await _dataContext.SaveChangesAsync();
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            _dataContext.Orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            return NoContent();

        }
    }
}