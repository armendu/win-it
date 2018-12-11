using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Role;

namespace Common.LogicInterfaces
{
    public interface IRoleLogic
    {
        Role GetRoleById(int roleId);

        List<Role> List();
        IEnumerable<Role> List2();

        Role Create(RoleViewModel role);
    }
}