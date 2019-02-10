using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Address
    {
        public Address()
        {
            UserInfo = new HashSet<UserInfo>();
        }

        public int AddressId { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
