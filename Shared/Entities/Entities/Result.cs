using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Result
    {
        public int ResultID { get; set; }

        [Required]
        public int GameID { get; set; }

        public int? LottoNumbers { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public virtual Game Game { get; set; }
    }
}