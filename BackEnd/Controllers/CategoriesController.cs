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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        public CategoriesController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategory(){
            return await  _dataContext.Categories.Select(x => new CategoryVm{
                Name = x.CategoryName,
                CategoryID = x.CategoryID
            }).ToListAsync();
            
            
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> GetCategory(int id ){
            var categories = await _dataContext.Categories.FindAsync(id);
            if (categories == null){
                return NotFound();
            }
            var category = new CategoryVm {
                CategoryID = categories.CategoryID,
                Name = categories.CategoryName
            };
            return category;
        }
        
        [HttpPost]
        public async Task<ActionResult<CategoryVm>> CreateCategory(CategoryFormVm model){
            var category = new Category {
                CategoryName = model.Name
            };
            _dataContext.Categories.Add(category);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction("Get Product",new {id = category.CategoryID}, new CategoryVm{CategoryID = category.CategoryID,Name = category.CategoryName });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryFormVm model){
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if (category == null){
                return NotFound();
            }
            category.CategoryName = model.Name;
            await _dataContext.SaveChangesAsync();
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteCategory(int id){
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryID ==id);
            if(category == null){
                return NotFound();
            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return NoContent();
            
        }
    }
}