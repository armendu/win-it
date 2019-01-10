using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.Role
{
    public class CreateRoleViewModel
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
    }
}