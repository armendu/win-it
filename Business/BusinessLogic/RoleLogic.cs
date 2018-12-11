using System;
using System.Collections.Generic;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Common.Helpers.Exceptions;
using Entities.Models;
using Entities.ViewModels.Role;

namespace BusinessLogic
{
    public class RoleLogic : IRoleLogic
    {
        private readonly IRoleRepository _roleRepository;

        public RoleLogic(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role GetRoleById(int roleId)
        {
            throw new System.NotImplementedException();
        }

        public List<Role> List()
        {
            try
            {
                return _roleRepository.List();
            }
            catch (Exception)
            {
                throw new ConnectionException();
            }
        }

        public IEnumerable<Role> List2()
        {
            try
            {
                return _roleRepository.List2();
            }
            catch (Exception)
            {
                throw new ConnectionException();
            }
        }

        public Role Create(RoleViewModel role)
        {
            try
            {
                var createdRole = _roleRepository.Create(role);
                return createdRole;
            }
            catch (Exception)
            {
                throw new ConnectionException();
            }
        }
    }
}