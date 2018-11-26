using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class UserInfo
    {
        public int UserInfoID { get; set; }

        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(120)]
        public string UserID { get; set; }

        [Required]
        public int AddressID { get; set; }

        public virtual Address Address { get; set; }
        public virtual User User { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }
    }
}