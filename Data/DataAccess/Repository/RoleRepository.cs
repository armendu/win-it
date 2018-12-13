using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.Role;

namespace DataAccess.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly EntityContext _entityContext;

        public RoleRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        /// <summary>
        /// Get role by Id.
        /// </summary>
        /// <returns>The retrieved role.</returns>
        public Role GetById(int id)
        {
            try
            {
                Role role = _entityContext.Roles.FirstOrDefault(r => r.RoleId == id);

                if (role != null)
                {
                    return role;
                }
                throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all the roles from the database.
        /// </summary>
        /// <returns>The retrieved roles as a list.</returns>
        public List<Role> List()
        {
            try
            {
                List<Role> roles = _entityContext.Roles.ToList();

                return roles;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public IEnumerable<Role> List2()
        {
            try
            {
                IEnumerable<Role> roles = _entityContext.Roles.AsEnumerable();

                return roles;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Persist a new role to the database.
        /// </summary>
        /// <param name="entity">The object to be persisted.</param>
        /// <returns>The persisted object.</returns>
        public Role Create(RoleViewModel entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    Role role = new Role
                    {
                        Name = entity.Name,
                        Description =  entity.Description,
                        CreatedAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };

                    _entityContext.Add(role);
                    _entityContext.SaveChanges();

                    transaction.Commit();
                    return role;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new System.NotImplementedException();
        }
    }
}