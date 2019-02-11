using System;
using System.Collections.Generic;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using Entities.ViewModels.Transaction;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class TransactionLogic: ITransactionLogic
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionLogic(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Transaction GetTransactionById(int transactionId)
        {
            try
            {
                return _transactionRepository.GetById(transactionId);
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("Transaction was not found");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public List<Transaction> List()
        {
            try
            {
                return _transactionRepository.List();
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("Transactions were not found");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public Transaction Create(CreateTransactionViewModel model)
        {
            try
            {
                return _transactionRepository.Create(model);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
            catch (Exception)
            {
                throw new OperationException("An error occured while creating Transaction!");
            }
        }
    }
}