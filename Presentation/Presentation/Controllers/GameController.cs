using System;
using System.Linq;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private readonly ILogger _logger;
        private const int PageSize = 4;

        public GameController(IGameLogic gameLogic, ILogger<GameController> logger)
        {
            _gameLogic = gameLogic;
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            try
            {
                IndexGameViewModel model = new IndexGameViewModel
                {
                    GamesList = _gameLogic.List()
                        .OrderBy(g => g.GameId)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = _gameLogic.List().Count
                    }
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Index");
            }
        }

        // GET: Game/Create
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

                return View("Create");
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

                return View("Create");
            }

            try
            {

                return
                    RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Create", model);
            }
        }

        public IActionResult Details()
        {
            var model = new IndexGameViewModel();
            return PartialView("_PartialDetails", model);
        }
    }
}