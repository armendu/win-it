using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
