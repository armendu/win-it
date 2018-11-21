using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class City
    {
        public int CityID { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}