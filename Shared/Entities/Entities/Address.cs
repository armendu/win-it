using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Address
    {
        public int AddressID { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [Required]
        public int CountryID { get; set; }

        [Required]
        public int CityID { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
    }
}