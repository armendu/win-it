using System;
using Common.LogicInterfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameLogic _gameLogic;

        public GameController(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: DayOff/Create
        public ActionResult Create()
        {
            try
            {
                return View("CreateView");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("CreateView");
            }
        }

        // POST: DayOff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Model State is not valid";

                return
                    View("CreateView");
            }

            try
            {

                return
                    RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return
                    View("CreateView", model);
            }
        }
    }
}