using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share;

namespace BackEnd.Controllers
{
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<CategoryVm>> GetCategory(){
            var categories = await _dataContext.Categories.Select(x => new CategoryVm{
                Name = x.CategoryName
            }).ToListAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryFormVm model){
            var category = new Category {
                CategoryName = model.Name
            };
            _dataContext.Categories.Add(category);
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryFormVm model){
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if (category == null){
                return NotFound();
            }
            category.CategoryName = model.Name;
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteCategory(int id){
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryID ==id);
            if(category == null){
                return NotFound();
            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return Accepted();
            
        }
    }
}