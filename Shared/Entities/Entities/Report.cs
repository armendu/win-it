using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Report
    {
        public int ReportID { get; set; }

        public int NumberOfGames { get; set; }

        public int NumberOfPlayers { get; set; }

        public int NumberOfWinners { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }
    }
}