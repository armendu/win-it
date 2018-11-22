using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class User
    {
        [Required]
        [StringLength(120)]
        public string UserID { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserInfo Profile { get; set; }
    }
}