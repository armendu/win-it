using System;
using System.Linq;
using System.Threading.Tasks;
using Common.LogicInterfaces;
using Entities.Models;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly ILogger _logger;
        private const int PageSize = 5;

        /// <summary>
        /// Creates a new instance of the UserController and injects the userLogic, userManager, singInManager, and logger.
        /// </summary>
        /// <param name="userLogic">The logic to be injected.</param>
        /// <param name="userManager">The user manager to be injected.</param>
        /// <param name="singInManager">The sign in to be injected.</param>
        /// <param name="logger">The logger to be injected.</param>
        public UserController(IUserLogic userLogic, UserManager<User> userManager,
            SignInManager<User> singInManager, ILogger<UserController> logger)
        {
            _userLogic = userLogic;
            _userManager = userManager;
            _singInManager = singInManager;
            _logger = logger;
        }

        // GET: User/Index
        public IActionResult Index(int page = 1)
        {
            IndexUserViewModel model = new IndexUserViewModel
            {
                Users = _userManager.Users
                    .OrderBy(g => g.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _userManager.Users.Count()
                }
            };

            return View(model);
        }

        // GET: User/Login
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View("Login", new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        // POST: User/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user =
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

        // GET: User/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        UserName = registerModel.FirstName,
                        Email = registerModel.Email,
                        IsActive = true
                    };
                    IdentityResult result
                        = await _userManager.CreateAsync(user, registerModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                    ViewBag.ErrorMessage = ex.Message;

                    return View("Create");
                }
            }

            return View(registerModel);
        }

        public IActionResult Details(User model)
        {
            User user = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);
            return PartialView("_PartialDetails", user);
        }

        // POST: User/Logout
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _singInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}