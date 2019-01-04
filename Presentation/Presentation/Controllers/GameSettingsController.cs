using System;
using System.Linq;
using Common.LogicInterfaces;
using Entities.ViewModels;
using Entities.ViewModels.GameSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class GameSettingsController : Controller
    {
        private readonly IGameSettingsLogic _gameSettingsLogic;
        private readonly ILogger _logger;
        private const int PageSize = 5;

        public GameSettingsController(IGameSettingsLogic gameSettingsLogic, ILogger<GameSettingsController> logger)
        {
            _gameSettingsLogic = gameSettingsLogic;
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            try
            {
                IndexGameSettingsViewModel model = new IndexGameSettingsViewModel
                {
                    GamesSettings = _gameSettingsLogic.List()
                        .OrderBy(g => g.GameSettingId)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = _gameSettingsLogic.List().Count
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
        public IActionResult Create(CreateGameSetting gameSetting)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Model State is not valid";

                return View("Create");
            }

            try
            {
                _gameSettingsLogic.Create(gameSetting);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Create", gameSetting);
            }
        }
    }
}