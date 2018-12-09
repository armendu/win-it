using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            Users = new HashSet<User>();
        }

        public int UserInfoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
