using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoCom.Model.Table;
using PhotoCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Controllers
{
    public class UserController : Controller
    {
        UserManager<USERS_TB> userManager;
        SignInManager<USERS_TB> signInManager;
        public UserController(UserManager<USERS_TB> userManager, SignInManager<USERS_TB> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login Page";
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Login(LoginVW model)
        {
            ViewData["Title"] = "Login Page";
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    model.RemmberME = true;
                    var res = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RemmberME, false);
                    if (res.Succeeded)
                    {

                        return RedirectToAction("index", "home");

                    }
                }
                ViewBag.error = "The password that you've entered is incorrect";
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            ViewData["Title"] = "Create Account";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(RegisterVM model)
        {
            ViewData["Title"] = "Create Account";

            if (ModelState.IsValid)
            {
                var user = new USERS_TB
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FIRST_NAME = model.FIRST_NAME,
                    LAST_NAME= model.LAST_NAME
                };
                var res = await userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: model.Remmberme);
                    return RedirectToAction("Index", "home");
                }

                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
