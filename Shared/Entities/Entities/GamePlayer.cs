using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Entities.Entities
{
    public class GamePlayer
    {
        public int GamePlayerID { get; set; }

        [Required]
        [StringLength(120)]
        public string UserID { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public virtual CreditInfo CreditInfo { get; set; }
        public virtual TransactionDetail Transactions { get; set; }
    }
}