using System.Collections.Generic;

namespace Entities.ViewModels.History
{
    public class IndexHistoryViewModel
    {
        public IEnumerable<Models.Transaction> Transactions { get; set; }
        public IEnumerable<Models.GameBet> GameBets { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}