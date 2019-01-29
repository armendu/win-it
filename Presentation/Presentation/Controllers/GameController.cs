using System;
using System.Linq;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        public GameController(IGameLogic gameLogic, ILogger<GameController> logger)
        {
            _gameLogic = gameLogic;
            _logger = logger;
        }

        // GET: Game/
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            try
            {
                IndexGameViewModel model = new IndexGameViewModel
                {
                    GamesList = _gameLogic.List()
                        .OrderByDescending(g => g.EndTime)
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

        // GET: Game/Create/
        [HttpGet]
        [Authorize]
        public IActionResult CreateGameBet(int id)
        {
            try
            {
                int gameId = _gameLogic.List().FirstOrDefault(g => g.GameProcessed == false)?.GameId ??
                             throw new NotFoundException();

                return View(new GameBetViewModel
                {
                    GameId = gameId
                });
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return RedirectToAction("Index");
            }
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateGameBet(Game model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Model State is not valid";

                return RedirectToAction("Index");
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
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Details(Game model)
        {
            try
            {
                return PartialView("_PartialDetails", model);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return RedirectToAction("Index");
            }
        }
    }
}