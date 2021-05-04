using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Share;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        public ProductsController(ApplicationDbContext dataContext, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/image", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProduct()
        {
            return await _dataContext.Products.Select(x => new ProductVm
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                CategoryID = x.CategoryID,
                Image = x.ProductImage,
                RatingAVG = x.RatingAverage,


            }).ToListAsync();


        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProduct(int id)
        {
            var products = await _dataContext.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            var product = new ProductVm
            {
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
        [AllowAnonymous]
        public async Task<ActionResult> CreateProduct([FromForm] ProductFormVm model)
        {
            if (model.ImageFile != null)
            {
                model.Image = await SaveImage(model.ImageFile);
            }
            var product = new Product
            {

                ProductName = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryID = model.CategoryID,
                RatingAverage = model.RatingAVG,
                ProductImage = model.Image

            };
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] ProductFormVm model)
        {
            if (model.ImageFile != null)
            {
                model.Image = await SaveImage(model.ImageFile);
            }
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryID = model.CategoryID;
            product.RatingAverage = model.RatingAVG;
            product.ProductImage = model.Image;


            await _dataContext.SaveChangesAsync();
            return NotFound();
        }
        [HttpPut]
        [Route("rating/{id}")]
        [AllowAnonymous]

        public async Task<ActionResult> UpdateRatingProduct(int id, ProductFormVm model)
        {
            if (model.ImageFile != null)
            {
                model.Image = await SaveImage(model.ImageFile);
            }
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryID = model.CategoryID;
            product.RatingAverage = model.RatingAVG;
            product.ProductImage = model.Image;


            await _dataContext.SaveChangesAsync();
            return NotFound();
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return NoContent();

        }



    }
}