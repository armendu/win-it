using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Transaction
    {
        public Transaction()
        {
            GameBets = new HashSet<GameBet>();
        }

        public int TransactionId { get; set; }
        public int PlayerId { get; set; }
        public decimal Sum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual ICollection<GameBet> GameBets { get; set; }
    }
}
