using System;
using System.Collections.Generic;
using System.Linq;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleLogic _roleLogic;
        private int PageSize = 2;

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

        public ViewResult List(int page = 1)
        {
            IndexRoleViewModel model = new IndexRoleViewModel
            {
                RolesList = _roleLogic.List2()
                    .OrderBy(r => r.RoleId)
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