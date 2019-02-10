using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Transaction;

namespace Common.LogicInterfaces
{
    public interface ITransactionLogic
    {
        Transaction GetTransactionById(int transactionId);

        List<Transaction> List();

        Transaction Create(CreateTransactionViewModel model);
    }
}