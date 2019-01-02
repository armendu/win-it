using System;
using System.Collections.Generic;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.User;

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
            User profile = _entityContext.Users.FirstOrDefault(u => u.Id.ToString() == id);

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

        public void Create(RegisterViewModel entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    Country country = new Country
                    {
                        Name = "Kosova",
                        UpdateAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    };

                    City city = new City
                    {
                        Name = "Prishtina",
                        Country = country,
                        UpdateAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    };

                    Address address = new Address
                    {
                        City = city,
                        Street = "Test street",
                        ZipCode = "10000",
                        UpdateAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    };

                    UserInfo userInfo = new UserInfo
                    {
                        Address = address,
                        UpdateAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Phone = entity.Phone,
//                        Birthdate = entity.Birthdate
                    };

                    Player player = new Player
                    {
                        Balance = 0,
                        NumberOfGamesPlayed = 0,
                        NumberOfGamesWon = 0,
                        TotalSpent = 0,
                        UpdateAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                    };

                    Role role = new Role
                    {
                        Name = "Initial role",
                        Description = "Entry role"
                    };

                    User user = new User
                    {
                        UserName = entity.Username,
                        UserInfo = userInfo,
                        Email = entity.Email,
                        Player = player
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