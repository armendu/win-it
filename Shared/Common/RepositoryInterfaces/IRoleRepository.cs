using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Role;

namespace Common.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        Role GetById(int id);
        List<Role> List();
        Role Create(RoleViewModel entity);
        void Update(Role entity);
        void Delete(Role entity);
    }
}