using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Player
    {
        public Player()
        {
            GameBets = new HashSet<GameBet>();
            GameWinners = new HashSet<GameWinner>();
            Transactions = new HashSet<Transaction>();
            Users = new HashSet<User>();
        }

        public int PlayerId { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalSpent { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public int NumberOfGamesWon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<GameBet> GameBets { get; set; }
        public virtual ICollection<GameWinner> GameWinners { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
