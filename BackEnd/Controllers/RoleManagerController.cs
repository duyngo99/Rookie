using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
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

        private readonly UserManager<User> userManager;
        public RoleManagerController(ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;

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
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id) {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) {
                ViewBag.ErrorMessage = $"User with ID  = {id} cannot be found";
                return View("NotFound");
            }

            var userClaims = await userManager.GetClaimsAsync(user);

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel {
                ID = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,

                Claims = userClaims.Select( c => c.Value).ToList(),
                Roles = userRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model){
            var user = await userManager.FindByIdAsync(model.ID);
            if (user == null) {
                ViewBag.ErrorMessage = $"User with ID = {model.ID} can not be found";
            }
            else
            {
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.UserName = model.UserName;
                
            }
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) {
                return RedirectToAction("ListUsers");
            }
            
            foreach(var error in result.Errors) {
                ModelState.AddModelError("",error.Description);
            }
            return View(model);
        }

    }
}