using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class CreditInfo
    {
        public int CreditInfoID { get; set; }

        [Required]
        [StringLength(120)]
        public string UserID { get; set; }

        [Required]
        [DefaultValue("0")]
        public string Total { get; set; }
        
        [Required]
        [DefaultValue("0")]
        public string OverallSpent { get; set; }

        [Required]
        [DefaultValue(0)]
        public int NumberOfGamesPlayed { get; set; }

        [Required]
        [DefaultValue(0)]
        public int NumberOfGamesWon { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public virtual TransactionDetail Transactions { get; set; }
    }
}