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
    public class ShoppingCartsController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        public ShoppingCartsController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ShoppingVm>>> GetShoppingCart(){
            return await  _dataContext.ShoppingCarts.Select(x => new ShoppingVm{
                ShoppingCartID = x.ShoppingCartID,
                ShoppingCartItemID = x.ShoppingCartItemID

            }).ToListAsync();
            
            
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ShoppingVm>> ShoppingCart(int id ){
            var shoppingcarts = await _dataContext.ShoppingCarts.FindAsync(id);
            if (shoppingcarts == null){
                return NotFound();
            }
            var shopping = new ShoppingVm {
                ShoppingCartID = shoppingcarts.ShoppingCartID,
                ShoppingCartItemID = shoppingcarts.ShoppingCartItemID
       
            };
            return shopping;
        }


        [HttpPost]
        public async Task<ActionResult<ProductVm>> PostShoppingCart(ShoppingFormVm model){
            var shopping = new ShoppingCart {
                
                ShoppingCartItemID = model.ShoppingCartItemID
                
            };
            _dataContext.ShoppingCarts.Add(shopping);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction("Get Product",new {id = shopping.ShoppingCartID}, new ShoppingVm{ShoppingCartID=shopping.ShoppingCartID,ShoppingCartItemID=shopping.ShoppingCartItemID});
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateShoppingCart(string id, ShoppingFormVm model){
            var shopping = await _dataContext.ShoppingCarts.FirstOrDefaultAsync(x => x.ShoppingCartID == id);
            if (shopping == null){
                return NotFound();
            }
            shopping.ShoppingCartItemID = model.ShoppingCartItemID;
            await _dataContext.SaveChangesAsync();
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteShoppingCart(string id){
            var shopping = await _dataContext.ShoppingCarts.FirstOrDefaultAsync(x => x.ShoppingCartID ==id);
            if(shopping == null){
                return NotFound();
            }
            _dataContext.ShoppingCarts.Remove(shopping);
            await _dataContext.SaveChangesAsync();
            return NoContent();
            
        }
    }
}