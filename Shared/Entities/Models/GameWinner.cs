using System;

namespace Entities.Models
{
    public class GameWinner
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int? NumbersMatches { get; set; }
        public decimal? SumWon { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
    }
}
