using System;
using System.Collections.Generic;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EntityContext _entityContext;

        public UserRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public User GetById(string id)
        {
            User profile = _entityContext.Users.FirstOrDefault(u => u.UserId == id);

            if (profile == null)
            {
                throw new NullReferenceException();
            }

            return profile;
        }

        public List<UserDetailsViewModel> List()
        {
            try
            {
                List<UserDetailsViewModel> allUsers = new List<UserDetailsViewModel>();

                if (allUsers.Count == 0)
                {
                    throw new NullReferenceException();
                }

                return allUsers;
            }
            catch
            {
                throw;
            }
        }

        public void Create(UserDetailsViewModel entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    User user = new User()
                    {
                        Email = "test@email.com",
                        Username = "username"
                    };
                    
                    _entityContext.Add(user);
                    _entityContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Login()
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    _entityContext.Users.Find();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return true;
        }

        public void Update(UserDetailsViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserDetailsViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}