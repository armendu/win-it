using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Rule
    {
        [Required]
        public string RuleID { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }
    }
}