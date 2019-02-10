using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.Game
{
    public class CreateGameBetViewModel
    {
        [Required]
        public List<int> Numbers { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int PlayerId { get; set; }
    }
}