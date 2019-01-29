using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RoleController : Controller
    {
        private readonly IRoleLogic _roleLogic;
        private const int PageSize = 10;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of the RoleController and injects the roleManager, and logger.
        /// </summary>
        /// <param name="roleLogic">The user logic to be injected.</param>
        /// <param name="logger">The logger to be injected.</param>
        public RoleController(IRoleLogic roleLogic, ILogger<RoleController> logger)
        {
            _roleLogic = roleLogic;
            _logger = logger;
        }

        // GET: Role/Index
        [HttpGet]
        public ViewResult Index(int page = 1)
        {
            try
            {
                IndexRoleViewModel model = new IndexRoleViewModel
                {
                    RolesList = _roleLogic.List()
                        .OrderBy(r => r.Id)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = _roleLogic.List().Count
                    }
                };

                return View(model);
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

            return View(null);
        }

        // GET: Role/Create
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View("Create");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return RedirectToAction("Index");
            }
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string name, string description)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IdentityResult result
                        = await _roleLogic.Create(name, description);

                    if (!result.Succeeded)
                        AddErrorsFromResult(result);

                    return RedirectToAction("Index");
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

            return View(name, description);
        }

        // GET: Role/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                EditRoleViewModel model = await _roleLogic.FindMembers(id);

                if (model == null)
                    return RedirectToAction("NotFoundError", "Home");

                return View("Edit", model);
            }
            catch (FormatException ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(null);
        }

        // POST: Role/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ModificationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IdentityResult changePasswordResult = await _roleLogic.Edit(model);

                    if (changePasswordResult != null)
                    {
                        if (!changePasswordResult.Succeeded)
                        {
                            AddErrorsFromResult(changePasswordResult);
                        }
                        else
                        {
                            ViewBag.Message = "The user roles were successfully modified.";
                        }

                        return await Edit(model.RoleId);
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

            return await Edit(model.RoleId);
        }

        public IActionResult Details(Role model)
        {
            try
            {
                return PartialView("_PartialDetails", model);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Index");
            }
        }

        // POST: Role/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Delete(string id)
        {
            try
            {
                IdentityResult result = await _roleLogic.Delete(id);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        TempData["ErrorMessage"] += error.Description + "\n";
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
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