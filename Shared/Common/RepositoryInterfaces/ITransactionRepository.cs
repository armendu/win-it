using System.Collections.Generic;
using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface ITransactionRepository
    {
        Transaction GetById(int id);
        List<Transaction> List();
        Transaction Create(Transaction entity);
        void Update(Transaction entity);
        void Delete(Transaction entity);
    }
}