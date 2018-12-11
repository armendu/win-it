using System;
using System.Collections.Generic;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleLogic _roleLogic;

        public RoleController(IRoleLogic roleLogic)
        {
            _roleLogic = roleLogic;
        }

        // GET: Role/Index
        public IActionResult Index()
        {
            try
            {
                List<Role> daysOff = _roleLogic.List();

                return
                    View("Index", daysOff);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return
                    View("Index");
            }
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            try
            {
                return View("Create");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Create");
            }
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Model State is not valid";

                return
                    View("Create");
            }

            try
            {
                _roleLogic.Create(model);
                return
                    RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return
                    View("Create", model);
            }
        }
    }
}