using System;
using System.Linq;
using System.Threading.Tasks;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admins")]
    public class ReportController : Controller
    {
        private readonly ITransactionLogic _transactionLogic;
        private readonly IUserLogic _userLogic;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        /// <summary>
        /// Creates a new instance of the TransactionController and injects the transactionLogic, and logger.
        /// </summary>
        /// <param name="transactionLogic">The transaction logic to be injected.</param>
        /// <param name="userLogic">The user logic to be injected.</param>
        /// <param name="logger">The logger to be injected.</param>
        public ReportController(ITransactionLogic transactionLogic, IUserLogic userLogic, ILogger<GameController> logger)
        {
            _transactionLogic = transactionLogic;
            _userLogic = userLogic;
            _logger = logger;
        }

        // GET: Transaction/
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            try
            {
                IndexTransactionViewModel model = new IndexTransactionViewModel
                {
                    Transactions = _transactionLogic.List()
                        .OrderBy(t => t.TransactionId)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = _transactionLogic.List().Count
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
    }
}