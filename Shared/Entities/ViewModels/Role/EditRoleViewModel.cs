using System.Collections.Generic;

namespace Entities.ViewModels.Role
{
    public class EditRoleViewModel
    {
        public Models.Role Role { get; set; }
        public IEnumerable<Models.User> Members { get; set; }
        public IEnumerable<Models.User> NonMembers { get; set; }
    }
}