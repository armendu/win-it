using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class TransactionDetail
    {
        public int TransactionDetailID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]
        public string Credit { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public virtual User User { get; set; }
    }
}