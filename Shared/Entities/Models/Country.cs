using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
