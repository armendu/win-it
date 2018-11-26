using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Game
    {
        public int GameID { get; set; }

        [Required]
        public double Pot { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime FinishTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public virtual List<GamePlayer> Players { get; set; }

        public virtual Result Result { get; set; }

        public virtual Rule Rule { get; set; }

        public virtual GameInfo GameInformation { get; set; }
    }
}