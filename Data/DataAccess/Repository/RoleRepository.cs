using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.Role;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EntityContext _entityContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleRepository(EntityContext entityContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _entityContext = entityContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<Role> FindById(string id)
        {
            try
            {
                Role role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                    throw new NullReferenceException();

                return role;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Role> List()
        {
            try
            {
                List<Role> roles = _roleManager.Roles.ToList();

                if (!roles.Any())
                    throw new NullReferenceException();

                return roles;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IdentityResult> Create(string name, string description)
        {
            try
            {
                return await _roleManager.CreateAsync(new Role(name, description));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EditRoleViewModel> FindMembers(string id)
        {
            try
            {
                Role role = await _roleManager.FindByIdAsync(id);
                List<User> members = new List<User>();
                List<User> nonMembers = new List<User>();

                foreach (User user in _userManager.Users)
                {
                    var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                    list.Add(user);
                }

                return new EditRoleViewModel
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult> Edit(ModificationRoleViewModel model)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    IdentityResult result = null;
                    foreach (string userId in model.IdsToAdd ?? new string[] { })
                    {
                        User user = await _userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            result = await _userManager.AddToRoleAsync(user,
                                model.RoleName);
                        }
                    }

                    foreach (string userId in model.IdsToDelete ?? new string[] { })
                    {
                        User user = await _userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            result = await _userManager.RemoveFromRoleAsync(user,
                                model.RoleName);
                        }
                    }

                    if (result == null)
                    {
                        throw new NullReferenceException();
                    }

                    if (!result.Succeeded)
                        transaction.Rollback();
                    else
                        transaction.Commit();

                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<IdentityResult> Delete(string id)
        {
            try
            {
                Role role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                    throw new NullReferenceException();

                foreach (User user in _userManager.Users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        return IdentityResult.Failed(new IdentityError
                        {
                            Description = "The role has active members"
                        });
                    }
                }

                return await _roleManager.DeleteAsync(role);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}