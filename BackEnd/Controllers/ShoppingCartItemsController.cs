using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share;

namespace BackEnd.Controllers
{
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        public ShoppingCartItemsController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemVm>>> GetShoppingCartITem(){
            return await  _dataContext.ShoppingCartItems.Select(x => new ShoppingCartItemVm{
                ShoppingCartID = x.ShoppingCartID, 
            }).ToListAsync();
            
            
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ShoppingCartItemVm>> GetShoppingCart(int id ){
            var shoppingCartItem = await _dataContext.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null){
                return NotFound();
            }
            var shoppingItem  = new ShoppingCartItemVm {
                ShoppingCartID = shoppingCartItem.ShoppingCartID,
                ShoppingCartItemID = shoppingCartItem.ShoppingCartItemID
                
            };
            return shoppingItem;
        }


        

    }
}