using System.Collections.Generic;
using System.Threading.Tasks;
using Common.LogicInterfaces;
using Entities.ViewModels;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public UserController(IUserLogic userLogic, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> singInManager)
        {
            _userLogic = userLogic;
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public IActionResult Index()
        {
            List<UserDetailsViewModel> model = _userLogic.List();

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string userId)
        {
            _userLogic.Create();

            return View();
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View("Login", new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user =
                    await _userManager.FindByNameAsync(loginModel.UserName);
                if (user != null)
                {
                    await _singInManager.SignOutAsync();
                    if ((await _singInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Index");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _singInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}