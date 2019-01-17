using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using Entities.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class RoleLogic: IRoleLogic
    {
        private readonly IRoleRepository _roleRepository;

        public RoleLogic(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> FindById(string id)
        {
            try
            {
                return await _roleRepository.FindById(id);
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("Role was not found");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public List<Role> List()
        {
            try
            {
                return _roleRepository.List();
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<IdentityResult> Create(string name, string description)
        {
            try
            {
                return await _roleRepository.Create(name, description);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<EditRoleViewModel> FindMembers(string id)
        {
            try
            {
                return await _roleRepository.FindMembers(id);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<IdentityResult> Edit(ModificationRoleViewModel model)
        {
            try
            {
                return await _roleRepository.Edit(model);
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("Role was not found");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<IdentityResult> Delete(string id)
        {
            try
            {
                return await _roleRepository.Delete(id);
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("User was not found");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }
    }
}