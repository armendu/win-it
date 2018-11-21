using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Country
    {
        public int CountryID { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }
    }
}