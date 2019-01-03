using System;
using System.Collections.Generic;
using System.Linq;
using Common.LogicInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionLogic _transactionLogic;
        private readonly ILogger _logger;
        private const int PageSize = 5;

        public TransactionController(ITransactionLogic transactionLogic, ILogger<GameController> logger)
        {
            _transactionLogic = transactionLogic;
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            try
            {
                IndexTransactionViewModel model = new IndexTransactionViewModel
                {
                    Transactions = _transactionLogic.List()
                        .OrderBy(g => g.GameId)
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