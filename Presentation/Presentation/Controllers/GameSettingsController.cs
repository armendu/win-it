using System;
using System.Linq;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.GameSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class GameSettingsController : Controller
    {
        private readonly IGameSettingsLogic _gameSettingsLogic;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        /// <summary>
        /// Creates a new instance of the GameSettingsController and injects the gameSettingsLogic and logger.
        /// </summary>
        /// <param name="gameSettingsLogic">The logic to be injected.</param>
        /// <param name="logger">The logger to be injected.</param>
        public GameSettingsController(IGameSettingsLogic gameSettingsLogic, ILogger<GameSettingsController> logger)
        {
            _gameSettingsLogic = gameSettingsLogic;
            _logger = logger;
        }

        // GET: GameSettings/
        [HttpGet]
        [Authorize(Roles = "Admins")]
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

        // GET: GameSettings/Create
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

                return View("Index");
            }
        }

        // POST: GameSettings/Create
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

        public IActionResult Details(GameSettings model)
        {
            return PartialView("_PartialDetails", model);
        }

        // GET: GameSettings/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                GameSettings gameSettings = _gameSettingsLogic.GetSettingsById(id);

                UpdateGameSetting updateGameSetting = new UpdateGameSetting
                {
                    GameSettingId = gameSettings.GameSettingId,
                    GameLength = gameSettings.GameLength,
                    UpdatedAt = gameSettings.UpdatedAt
                };

                return View("Edit", updateGameSetting);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"The following error occurred: {ex.Message} @ {GetType().Name}");
                ViewBag.ErrorMessage = ex.Message;

                return View("Index");
            }
        }

        // POST: GameSettings/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateGameSetting entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _gameSettingsLogic.Update(entity);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    View(exception.Message);
                }
            }

            return View();
        }
    }
}