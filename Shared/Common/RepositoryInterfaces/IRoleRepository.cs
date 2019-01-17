using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels.Role;
using Microsoft.AspNetCore.Identity;

namespace Common.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        Task<Role> FindById(string id);
        List<Role> List();
        Task<IdentityResult> Create(string name, string description);
        Task<EditRoleViewModel> FindMembers(string id);
        Task<IdentityResult> Edit(ModificationRoleViewModel model);
        Task<IdentityResult> Delete(string id);
    }
}