using System.Collections.Generic;
using Common.RepositoryInterfaces;
using Entities.Models;

namespace DataAccess.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        public Transaction GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Transaction> List()
        {
            throw new System.NotImplementedException();
        }

        public Transaction Create(Transaction entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Transaction entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Transaction entity)
        {
            throw new System.NotImplementedException();
        }
    }
}