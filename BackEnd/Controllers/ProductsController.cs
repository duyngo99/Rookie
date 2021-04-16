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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        public ProductsController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProduct(){
            return await  _dataContext.Products.Select(x => new ProductVm{
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                Description = x.Description,
                Price=x.Price,
                CategoryID = x.CategoryID,
                Image = x.ProductImage,
                RatingAVG = x.RatingAverage
                
            }).ToListAsync();
            
            
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProduct(int id ){
            var products = await _dataContext.Products.FindAsync(id);
            if (products == null){
                return NotFound();
            }
            var product = new ProductVm {
                ProductID = products.ProductID,
                ProductName = products.ProductName,
                Price = products.Price,
                Description = products.Description,
                CategoryID = products.CategoryID,
                Image = products.ProductImage,
                RatingAVG = products.RatingAverage
                
            };
            return product;
        }


        [HttpPost]
        public async Task<ActionResult<ProductVm>> CreateProduct(ProductFormVm model){
            var product = new Product {
                
                ProductName = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryID = model.CategoryID,
                RatingAverage = model.RatingAVG
                
            };
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction("Get Product",new {id = product.ProductID}, new ProductVm{ProductID = product.ProductID,ProductName = product.ProductName});
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
            product.RatingAverage = model.RatingAVG;
            await _dataContext.SaveChangesAsync();
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteProduct(int id){
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductID ==id);
            if(product == null){
                return NotFound();
            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return NoContent();
            
        }


        
    }
}