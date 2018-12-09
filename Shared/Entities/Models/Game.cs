using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Game
    {
        public Game()
        {
            Gamebets = new HashSet<GameBet>();
            Gamewinners = new HashSet<GameWinner>();
            Transactions = new HashSet<Transaction>();
        }

        public int GameId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? GameInfoId { get; set; }

        public virtual GameInfo GameInfo { get; set; }
        public virtual ICollection<GameBet> Gamebets { get; set; }
        public virtual ICollection<GameWinner> Gamewinners { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
