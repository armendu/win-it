using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers.Exceptions;
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
                Transaction role = _entityContext.Transactions.FirstOrDefault(t => t.TransactionId == id);

                if (role == null)
                    throw new NullReferenceException();

                return role;
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
                    throw new NotFoundException("There are cities to be shown!");

                return transactions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Transaction Create(CreateTransactionViewModel model)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    Transaction transactionModel = new Transaction
                    {
                        PlayerId = model.PlayerId,
                        GameId = model.GameId,
                        Sum = model.Sum,
                        CreatedAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };

                    _entityContext.Add(transactionModel);
                    _entityContext.SaveChanges();

                    transaction.Commit();

                    return transactionModel;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Transaction model)
        {
            throw new System.NotImplementedException();
        }
    }
}