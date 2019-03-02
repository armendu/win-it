using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly ICitiesLogic _citiesLogic;
        private readonly SignInManager<User> _singInManager;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        /// <summary>
        /// Creates a new instance of the UserController and injects the userLogic, userManager, singInManager, and logger.
        /// </summary>
        /// <param name="userLogic">The user logic to be injected.</param>
        /// <param name="citiesLogic">The cities logic to be injected.</param>
        /// <param name="singInManager">The sign in manager to be injected.</param>
        /// <param name="logger">The logger to be injected.</param>
        public UserController(IUserLogic userLogic, ICitiesLogic citiesLogic, SignInManager<User> singInManager,
            ILogger<UserController> logger)
        {
            _userLogic = userLogic;
            _citiesLogic = citiesLogic;
            _singInManager = singInManager;
            _logger = logger;
        }

        // GET: User/
        [HttpGet]
        [Authorize(Roles = "Admins")]
        public IActionResult Index(int page = 1)
        {
            try
            {
                IndexUserViewModel model = new IndexUserViewModel
                {
                    Users = _userLogic.List()
                        .OrderBy(g => g.Id)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = _userLogic.List().Count
                    }
                };

                return View(model);
            }
            catch (ConnectionException ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (NotFoundException ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"A general exception occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(null);
        }

        // GET: User/Login
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            try
            {
                return View(new LoginViewModel
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

        // POST: User/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _userLogic.Login(loginModel))
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }

                    _logger.Log(LogLevel.Error, "Invalid username or password");
                    ModelState.AddModelError("", "Invalid username or password");

                    return View(loginModel);
                }
                catch (ConnectionException ex)
                {
                    _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (NullReferenceException ex)
                {
                    _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"A general exception occurred: {ex.Message} @ {GetType().Name}");
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(loginModel);
        }

        // GET: User/Create
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create(string returnUrl)
        {
            try
            {
                ViewBag.Countries = new SelectList(_citiesLogic.ListCountries());

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var birthdate = DateTime.Parse(registerModel.Birthdate);

                    if (birthdate.Age() < 18)
                        throw new Exception("You have to be at least 18 years old to register");

                    RegisterResultViewModel registerResult = await _userLogic.Create(registerModel);

                    if (registerResult != null)
                    {
                        if (registerResult.Result.Succeeded)
                        {
                            if ((await _singInManager.PasswordSignInAsync(registerResult.User,
                                registerModel.Password, false, false)).Succeeded)
                            {
                                return Redirect(registerModel?.ReturnUrl ?? "/");
                            }
                        }
                        else
                        {
                            AddErrorsFromResult(registerResult.Result);
                        }
                    }
                }
                catch (ConnectionException ex)
                {
                    _logger.Log(LogLevel.Error,
                        $"The following connection error occurred: {ex.Message} @ {GetType().Name}");
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"A general exception occurred: {ex.Message} @ {GetType().Name}");
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Countries = new SelectList(_citiesLogic.ListCountries());

            return View(registerModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCitiesForCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return null;

            List<City> cities = _citiesLogic.List(country);
            
            return Json(cities);
        }

        // GET: User/ChangePassword/{id}
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                User currentUser = await _userLogic.GetCurrentUser(HttpContext.User);

                ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel
                {
                    Id = currentUser.Id,
                    Email = currentUser.Email
                };

                return View(changePasswordViewModel);
            }
            catch (NotFoundException ex)
            {
                _logger.Log(LogLevel.Error,
                    $"The requested resource was not found with the following message: {ex.Message} @ {GetType().Name}");
            }
            catch (ConnectionException ex)
            {
                _logger.Log(LogLevel.Error,
                    $"The following connection error occurred: {ex.Message} @ {GetType().Name}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"A general exception occurred: {ex.Message} @ {GetType().Name}");
            }

            return RedirectToAction("NotFoundError", "Home");
        }

        // POST: User/ChangePassword/{id}
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User currentUser = await _userLogic.GetCurrentUser(HttpContext.User);

                    if (currentUser.Id == model.Id)
                    {
                        IdentityResult changePasswordResult = await _userLogic.ChangePassword(model);

                        if (changePasswordResult != null)
                        {
                            if (!changePasswordResult.Succeeded)
                                AddErrorsFromResult(changePasswordResult);

                            return Redirect(model?.ReturnUrl ?? "/");
                        }
                    }
                }
                catch (NotFoundException ex)
                {
                    _logger.Log(LogLevel.Error,
                        $"The requested resource was not found with the following message: {ex.Message} @ {GetType().Name}");
                }
                catch (ArgumentException ex)
                {
                    _logger.Log(LogLevel.Error,
                        $"The requested resource was not found with the following message: {ex.Message} @ {GetType().Name}");
                }
                catch (ConnectionException ex)
                {
                    _logger.Log(LogLevel.Error,
                        $"The following connection error occurred: {ex.Message} @ {GetType().Name}");
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"A general exception occurred: {ex.Message} @ {GetType().Name}");
                }
            }

            return View("ChangePassword", model);
        }

        // GET: User/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            User user = await _userLogic.GetCurrentUser(HttpContext.User);
            if (user != null)
            {
                EditUserViewModel changePasswordViewModel = new EditUserViewModel
                {
                    Id = user.Id,
                    Birthdate = user.UserInfo.Birthdate.ToString(),
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName,
                    Phone = user.UserInfo.Phone
                };

                return View(changePasswordViewModel);
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
            try
            {

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Index");
            }
        }

        public async Task<IActionResult> Details(User model)
        {
            try
            {
                User user = await _userLogic.FindById(model.Id.ToString());

                if (user == null)
                    throw new Exception("User not found!");

                return PartialView("_PartialDetails", user);
            }
            catch (NotFoundException ex)
            {
                _logger.Log(LogLevel.Error,
                    $"The requested resource was not found with the following message: {ex.Message} @ {GetType().Name}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }

            return RedirectToAction("NotFoundError", "Home");
        }

        public async Task<IActionResult> Profile()
        {
            try
            {
                User user = await _userLogic.GetCurrentUser(HttpContext.User);

                if (user == null)
                    throw new Exception("User not found!");

                EditUserViewModel userData = new EditUserViewModel
                {
                    Id = user.Id,
                    Birthdate = user.UserInfo.Birthdate.ToString(),
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName,
                    Phone = user.UserInfo.Phone
                };

                return View(userData);
            }
            catch (NotFoundException ex)
            {
                _logger.Log(LogLevel.Error,
                    $"The requested resource was not found with the following message: {ex.Message} @ {GetType().Name}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }

            return RedirectToAction("NotFoundError", "Home");
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
                _logger.Log(LogLevel.Error,
                    $"The following error occurred: {error.Description} @ {GetType().Name}");
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}