using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IUserLogic _userLogic;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        public GameController(IGameLogic gameLogic, IUserLogic userLogic, ILogger<GameController> logger)
        {
            _gameLogic = gameLogic;
            _userLogic = userLogic;
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
        public async Task<IActionResult> CreateGameBet()
        {
            try
            {
                int gameId = _gameLogic.List().FirstOrDefault(g => g.GameProcessed == false)?.GameId ??
                             throw new NotFoundException();

                User user = await _userLogic.GetCurrentUser(HttpContext.User);

                GameBetViewModel model = new GameBetViewModel
                {
                    Numbers = new List<int>(7),
                    PlayerId = user.PlayerId ?? 0,
                    PlayersBalance = user.Player.Balance,
                    GameId = gameId
                };

                for (int i = 0; i < model.Numbers.Capacity; i++)
                {
                    model.Numbers.Add(1);
                }

                return View(model);
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
        public IActionResult CreateGameBet(GameBetViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if there are duplicate numbers; throw if there are any
                    if (model.Numbers.Distinct().Count() != model.Numbers.Count)
                        throw new FormatException("The numbers can't be the same");

                    CreateGameBetViewModel gameBet = new CreateGameBetViewModel
                    {
                        Numbers = model.Numbers,
                        Sum = model.Sum,
                        GameId = model.GameId,
                        PlayerId = model.PlayerId
                    };

                    _gameLogic.CreateGameBet(gameBet);

                    return
                        RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
                ModelState.AddModelError("", "Model State is not valid");

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