using BackEnd.Controllers;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Share;

namespace UnitTest
{
    public class CategoryUnitTest
    {
        private ApplicationDbContext _dbContext;
        private SqliteConnection _connection;

        public CategoryUnitTest()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            _connection = new SqliteConnection(connectionStringBuilder.ToString());
            _connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();
        }
        [Fact]
        public async Task CreateCategory_ReturnCategoryModel()
        {
            var category = new CategoryFormVm
            {
                Name = "Test"
            };
            var controller = new CategoriesController(_dbContext);
            var result = await controller.CreateCategory(category);

            var acceptedResult = Assert.IsType<OkObjectResult>(result);
            

            Assert.Equal("Test", acceptedResult.Value);
        }

        [Fact]
        public async Task GetAllCategories_OkResult_4Items()
        {

            for (int i = 0; i < 4; i++)
            {
                _dbContext.Categories.Add(new Category
                {
                    CategoryName = "Test",

                });
                await _dbContext.SaveChangesAsync();
            }
            var controller = new CategoriesController(_dbContext);

            var result = await controller.GetCategory();
            var categoryItems = result.Value as List<CategoryVm>;
            Assert.Equal(4, categoryItems.Count);
        }

        [Fact]
        public async Task getCategoryById_OkResult_RightItem()
        {
            _dbContext.Categories.Add(new Category
            {
                CategoryID = 666,
                CategoryName = "TestName",

            });
            await _dbContext.SaveChangesAsync();

            var controller = new CategoriesController(_dbContext);
            var result = await controller.GetCategory(666);

            var okResult = Assert.IsType<ActionResult<CategoryVm>>(result);
            var categoryItem = Assert.IsType<CategoryVm>(result.Value);
            Assert.NotNull(categoryItem);
            Assert.Equal("TestName", categoryItem.Name);
            Assert.Equal(666, categoryItem.CategoryID);


        }
        [Fact]
        public async Task getCategoryById_NotFoundResult()
        {
            _dbContext.Categories.Add(new Category
            {
                CategoryID = 666,
                CategoryName = "TestName"
            });
            await _dbContext.SaveChangesAsync();

            var controller = new CategoriesController(_dbContext);
            var result = await controller.GetCategory(999999999);
            

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task deleteCategoryById_AcceptedResult()
        {

            _dbContext.Categories.Add(new Category
            {
                CategoryID = 666,
                CategoryName = "TestName",

            });
            await _dbContext.SaveChangesAsync();

            var controller = new CategoriesController(_dbContext);
            var deleteCategory = await controller.DeleteCategory(666);
            Assert.IsType<AcceptedResult>(deleteCategory);

            var result = await controller.GetCategory();
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task deleteCategoryById_NotfountResult()
        {

            _dbContext.Categories.Add(new Category
            {
                CategoryID = 666,
                CategoryName = "TestName",

            });
            await _dbContext.SaveChangesAsync();

            var controller = new CategoriesController(_dbContext);
            var deleteCategory = await controller.DeleteCategory(699);
            Assert.IsType<NotFoundResult>(deleteCategory);


        }
        [Fact]
        public async Task UpdateCategoryById_FoundResult()
        {
            _dbContext.Categories.Add(new Category
            {
                CategoryID = 999,
                CategoryName = "TestName"
            });
            await _dbContext.SaveChangesAsync();
            int categoryID = 666;
            string name = "TestAgain";
            var duplicateCategory = new CategoryFormVm { Name = "TestAgain" };
            var controller = new CategoriesController(_dbContext);
            var result = await controller.UpdateCategory(categoryID, duplicateCategory);

            Assert.Equal(name, duplicateCategory.Name);
        }
        [Fact]
        public async Task UpdateCategoryById_NotfoundResult()
        {
            int notfountId = 696969;
            var categoryModel = new CategoryFormVm { Name = "UpdateName" };
            var controller = new CategoriesController(_dbContext);
            var result = await controller.UpdateCategory(notfountId, categoryModel);
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
