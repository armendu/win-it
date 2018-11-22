using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class Role
    {
        public int RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}