using System.Collections.Generic;

namespace Entities.ViewModels.Role
{
    public class IndexRoleViewModel
    {
        public IEnumerable<Models.Role> RolesList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}