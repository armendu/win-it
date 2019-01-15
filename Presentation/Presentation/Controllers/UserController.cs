﻿using System;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly IUserValidator<User> _userValidator;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        /// <summary>
        /// Creates a new instance of the UserController and injects the userLogic, userManager, singInManager, and logger.
        /// </summary>
        /// <param name="userManager">The user manager to be injected.</param>
        /// <param name="singInManager">The sign in manager to be injected.</param>
        /// <param name="userValidator">The userValidator to be injected.</param>
        /// <param name="passwordValidator">The passwordValidator to be injected.</param>
        /// <param name="passwordHasher">The passwordHasher to be injected.</param>
        /// <param name="logger">The logger to be injected.</param>
        public UserController(UserManager<User> userManager,
            SignInManager<User> singInManager, IUserValidator<User> userValidator,
            IPasswordValidator<User> passwordValidator, IPasswordHasher<User> passwordHasher,
            ILogger<UserController> logger)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        // GET: User/
        [HttpGet]
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
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        // GET: User/Create
        [HttpGet]
        public IActionResult Create(string returnUrl)
        {
            try
            {
                return View(new RegisterViewModel
                {
                    ReturnUrl = returnUrl
                });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Index");
            }
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
                        if ((await _singInManager.PasswordSignInAsync(user,
                            registerModel.Password, false, false)).Succeeded)
                        {
                            return Redirect(registerModel?.ReturnUrl ?? "/");
                        }
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            _logger.Log(LogLevel.Error, $"The following error occurred: {error.Description} @ {GetType().Name}");
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                    ViewBag.ErrorMessage = ex.Message;

                    return RedirectToAction("Index");
                }
            }

            return View(registerModel);
        }

        // GET: User/ChangePassword/{id}
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction("NotFoundError", "Home");

            ChangePasswordViewModel editUser = new ChangePasswordViewModel
            {
                Id = user.Id,
                Email = user.Email
            };

            return View(editUser);
        }

        // POST: User/ChangePassword/{id}
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            User user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user != null)
            {
                user.Email = model.Email;

                IdentityResult validEmail
                    = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.ReTypePassword) &&
                    model.Password.Equals(model.ReTypePassword))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager,
                        user, model.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user,
                            model.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null)
                    || (validEmail.Succeeded
                        && model.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                        AddErrorsFromResult(result);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View(model);
        }

        // GET: User/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                EditUserViewModel editUser = new EditUserViewModel
                {

                };

                return View(editUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: User/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            return View(model);
        }

        public IActionResult Details(User model)
        {
            try
            {
                User user = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);

                if (user != null)
                    return PartialView("_PartialDetails", user);
                else
                    throw new Exception("User not found!");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Index");
            }
        }

        // POST: User/Logout
        [HttpPost]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _singInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}