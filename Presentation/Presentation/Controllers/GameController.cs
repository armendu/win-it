using System;
using System.Linq;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private const int PageSize = 4;

        public GameController(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        public IActionResult Index(int page = 1)
        {
            IndexGameViewModel model = new IndexGameViewModel
            {
                GamesList = _gameLogic.GetGamesList()
                    .OrderBy(g => g.GameId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _gameLogic.GetGamesList().Count
                }
            };

            return View(model);
        }

        // GET: Game/Create
        public IActionResult Create()
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

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game model)
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

        public IActionResult Details()
        {
            var model = new IndexGameViewModel();
            return PartialView("_PartialDetails", model);
        }
    }
}