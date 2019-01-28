using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Game
    {
        public Game()
        {
            GameBets = new HashSet<GameBet>();
            GameWinners = new HashSet<GameWinner>();
            Transactions = new HashSet<Transaction>();
        }

        public int GameId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool GameProcessed { get; set; }
        public int GameInfoId { get; set; }

        public virtual GameInfo GameInfo { get; set; }
        public virtual ICollection<GameBet> GameBets { get; set; }
        public virtual ICollection<GameWinner> GameWinners { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
