using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IGameLogic _gameLogic;
        private readonly IGameSettingsLogic _gameSettingsLogic;
        private readonly IPlayerLogic _playerLogic;
        private readonly ILogger _logger;

        public HomeController(IUserLogic userLogic, IGameLogic gameLogic, IGameSettingsLogic gameSettingsLogic, IPlayerLogic playerLogic, ILogger<HomeController> logger)
        {
            _userLogic = userLogic;
            _gameLogic = gameLogic;
            _gameSettingsLogic = gameSettingsLogic;
            _playerLogic = playerLogic;
            _logger = logger;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            User currentUser = null;
            try
            {
                currentUser = await _userLogic.GetCurrentUser(HttpContext.User);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Warning, $"The {ex.Message} @ {GetType().Name}");
            }

            try
            {
                // Get the number of the registered users, the end time of the current running game.
                int registeredUsers = _userLogic.List().Count;
                DateTime currentGameEndTime = _gameLogic.GetRunningGame()?.EndTime ?? DateTime.UtcNow;

                // Display the user's balance if he/she is logged in.
                decimal playersBalance = currentUser?.Player?.Balance ?? 0;

                // Calculate the minutes til the next game starts.
                int timeTillGameEnds = (int)(currentGameEndTime - DateTime.UtcNow).TotalMinutes;

                // Get the pot of the running game.
                decimal currentPot = _gameLogic.GetRunningGame()?.GameInfo?.WinningPot ?? 0;

                return View(new DashboardViewModel
                {
                    RegisteredUsers = registeredUsers,
                    MinutesTillNextGame = timeTillGameEnds.ToString(),
                    PlayersBalance = playersBalance > 0 ? playersBalance.ToString("C") : "Login to see balance",
                    CurrentPotOfGame = currentPot
                });
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult NotFoundError()
        {
            return View();
        }
    }
}
