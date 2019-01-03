using System.Collections.Generic;
using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface ITransactionLogic
    {
        Transaction GetTransactionById(int transactionId);

        List<Transaction> List();

        void Create();
    }
}