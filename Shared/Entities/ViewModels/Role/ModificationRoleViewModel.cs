using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels.Role
{
    public class ModificationRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}