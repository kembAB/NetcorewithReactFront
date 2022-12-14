using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebApp.Models;
using MVCWebApp.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    //Role controller controlles autorization 
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult AddUserToRole()
        {
            AddToRoleViewModel model = new AddToRoleViewModel();
            model.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            model.Users = new SelectList(_userManager.Users, "Id", "UserName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(string role, string user)
        {
            var tempUser = await _userManager.FindByIdAsync(user);

            IdentityResult result = await _userManager.AddToRoleAsync(tempUser, role);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

        
            AddToRoleViewModel model = new AddToRoleViewModel();
            model.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            model.Users = new SelectList(_userManager.Users, "Id", "UserName");
            if(result.Errors.Any())
            {
                foreach (var item in result.Errors)
                {
                    //Todo-> change to array 
                    model.Message += item.Description;
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            string errorMessage = "";

            if(ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    if (result.Errors.Any())
                {
                    foreach (var item in result.Errors)
                    {
                        //Todo-> change to array 
                        errorMessage += item.Description;
                    }
                }
            }

           
            ViewData["errorMessage"] = errorMessage;

            return View();
        }
    }
}
