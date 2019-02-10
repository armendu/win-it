using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.Game
{
    public class GameBetViewModel
    {
        [Required] public int GameId { get; set; }

        [Required] public int PlayerId { get; set; }

        [Required] [Range(1, double.MaxValue)] public decimal Sum { get; set; }

        public decimal PlayersBalance { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        public List<int> Numbers { get; set; }
    }
}