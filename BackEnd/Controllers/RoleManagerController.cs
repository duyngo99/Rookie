using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public RoleManagerController(ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            this.roleManager = roleManager;
        }


        [Authorize]
        [HttpGet]
        public IActionResult CreateRole(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model){

                if (ModelState.IsValid) {
                    IdentityRole identityRole = new IdentityRole {
                        Name = model.RoleName
                    };
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded){
                        return RedirectToAction("ListRoles","RoleManager");
                    }

                    foreach(IdentityError error in result.Errors) {
                        ModelState.AddModelError("",error.Description);
                    }
                }
                return  View(model);
        }

        [Authorize]
        public IActionResult ListRoles(){
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id){
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) {
                ViewBag.ErrorMessage = $"Role with ID  = {id} cannot be found";
                return View("NotFound");
            }

            else {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) {
                    return RedirectToAction("ListRoles");

                }
                foreach ( var error in result.Errors) {
                    ModelState.AddModelError("",error.Description);
                }
                return View("ListRoles");

            }
            
        }
    }
}