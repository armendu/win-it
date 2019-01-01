using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.LogicInterfaces;
using Entities.ViewModels;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly ILogger _logger;

        public UserController(IUserLogic userLogic, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> singInManager, ILogger<UserController> logger)
        {
            _userLogic = userLogic;
            _userManager = userManager;
            _singInManager = singInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<UserDetailsViewModel> model = _userLogic.List();

            return View(model);
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userLogic.Create(registerModel);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                    ViewBag.ErrorMessage = ex.Message;

                    return View("Create");
                }
            }

            ViewBag.ErrorMessage = "Model state is not valid";
            return View("Create");
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _singInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}