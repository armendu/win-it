using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Transaction;

namespace Common.RepositoryInterfaces
{
    public interface ITransactionRepository
    {
        Transaction GetById(int id);
        List<Transaction> List();
        Transaction Create(CreateTransactionViewModel model);
        void Update(Transaction model);
    }
}