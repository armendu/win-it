using System.Collections.Generic;

namespace Entities.ViewModels.Transaction
{
    public class IndexTransactionViewModel
    {
        public IEnumerable<Models.Transaction> Transactions { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}