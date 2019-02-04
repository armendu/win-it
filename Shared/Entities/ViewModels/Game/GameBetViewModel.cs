using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.Game
{
    public class GameBetViewModel
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int FirstNumber { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int SecondNumber { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int ThirdNumber { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int FourthNumber { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int FifthNumber { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int SixthNumber { get; set; }

        [Required(ErrorMessage = "Please provide a number between 1 and 39.")]
        [Range(1, 39)]
        public int SeventhNumber { get; set; }
    }
}