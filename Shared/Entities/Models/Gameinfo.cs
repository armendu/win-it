using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class GameInfo
    {
        public GameInfo()
        {
            Games = new HashSet<Game>();
        }

        public int GameInfoId { get; set; }
        public decimal? WinningPot { get; set; }
        public string WinningNumbers { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
