using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProductsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<ProductVm>> GetProduct(){
            var products = await _dataContext.Products.Select(x => new ProductVm{
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                CategoryID = x.CategoryID
            }).ToListAsync();
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductFormVm model){
            var product = new Product {
                ProductName = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryID = model.CategoryID
            };
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductFormVm model){
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (product == null){
                return NotFound();
            }
            product.ProductName = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryID = model.CategoryID;
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteProduct(int id){
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductID ==id);
            if(product == null){
                return NotFound();
            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return Accepted();
            
        }
    }
}