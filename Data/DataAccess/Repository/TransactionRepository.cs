using System;
using System.Collections.Generic;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.Transaction;

namespace DataAccess.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly EntityContext _entityContext;

        public TransactionRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public Transaction GetById(int id)
        {
            try
            {
                Transaction transaction = _entityContext.Transactions.FirstOrDefault(t => t.TransactionId == id);

                if (transaction == null)
                    throw new NullReferenceException();

                return transaction;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Transaction> List()
        {
            try
            {
                List<Transaction> transactions = _entityContext.Transactions.ToList();

                if (transactions.Count == 0)
                    throw new NullReferenceException();

                return transactions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Transaction Create(CreateTransactionViewModel model)
        {
            try
            {
                // TODO: Check the balance before doing a transaction
                Transaction transactionModel = new Transaction
                {
                    PlayerId = model.PlayerId,
                    Sum = model.Sum,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                return transactionModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Transaction model)
        {
            throw new System.NotImplementedException();
        }
    }
}