using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class GameInfo
    {
        public int GameInfoID { get; set; }

        [Required]
        public int GameID { get; set; }

        [DefaultValue(0)]
        public int NumberOfWinners { get; set; }

        [DefaultValue(0)]
        public int NumberOfPlayers { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public virtual Game Game { get; set; }
    }
}