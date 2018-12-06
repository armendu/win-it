using System.Collections.Generic;
using Common.LogicInterfaces;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IActionResult Index()
        {
            List<UserDetailsViewModel> model = _userLogic.GetUsersList();

            return View(model);
        }
    }
}