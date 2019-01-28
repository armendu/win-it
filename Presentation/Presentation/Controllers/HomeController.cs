using System;
using System.Diagnostics;
using System.Linq;
using Common.LogicInterfaces;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IGameLogic _gameLogic;

        public HomeController(IUserLogic userLogic, IGameLogic gameLogic)
        {
            _userLogic = userLogic;
            _gameLogic = gameLogic;
        }

        // GET: Index
        public IActionResult Index()
        {
            try
            {
                int registeredUsers = _userLogic.List().Count;
                DateTime currentGameEndTime = _gameLogic.List().FirstOrDefault(g => g.GameProcessed == false)?.EndTime ?? DateTime.UtcNow;

                int timeUntilNewGame = (int)(currentGameEndTime - DateTime.UtcNow).TotalMinutes;

                return View(new DashboardViewModel
                {
                    RegisteredUsers = registeredUsers,
                    MinutesTillNextGame = timeUntilNewGame.ToString()
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
