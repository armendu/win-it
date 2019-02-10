using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.Transaction
{
    public class CreateTransactionViewModel
    {
        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public decimal Sum { get; set; }
    }
}