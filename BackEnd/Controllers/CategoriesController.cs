using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]

    public class CategoriesController : ControllerBase
    {

        private readonly ApplicationDbContext _dataContext;
        public CategoriesController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategory()
        {
            var categories = await _dataContext.Categories.Select(x => new CategoryVm
            {
                Name = x.CategoryName,
                CategoryID = x.CategoryID
            }).ToListAsync();
            if (categories == null)
            {
                return NotFound();
            }
            return categories;
        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<CategoryVm>> GetCategory(int id)
        {
            var categories = await _dataContext.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            var category = new CategoryVm
            {
                CategoryID = categories.CategoryID,
                Name = categories.CategoryName
            };
            return category;
        }

        [HttpPost]
        [Authorize("Admin")]

        public async Task<ActionResult> CreateCategory([FromForm] CategoryFormVm model)
        {
            var category = new Category
            {
                CategoryName = model.Name
            };
            _dataContext.Categories.Add(category);
            await _dataContext.SaveChangesAsync();
            return Ok(category.CategoryName);
        }
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult> UpdateCategory(int id, [FromForm] CategoryFormVm model)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            category.CategoryName = model.Name;
            await _dataContext.SaveChangesAsync();
            return Accepted();
        }
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return Accepted();

        }
    }
}