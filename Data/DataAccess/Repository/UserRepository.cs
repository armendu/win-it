using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EntityContext _entityContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;

        public UserRepository(EntityContext entityContext, UserManager<User> userManager,
            SignInManager<User> singInManager)
        {
            _entityContext = entityContext;
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public async Task<User> FindById(string id)
        {
            try
            {
                User profile = await _userManager.FindByIdAsync(id);

                if (profile == null)
                    throw new NullReferenceException();

                return profile;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> List()
        {
            try
            {
                List<User> users = _userManager.Users.ToList();

                if (!users.Any())
                    throw new NullReferenceException();

                return users;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Login(LoginViewModel loginModel)
        {
            try
            {
                User user =
                    await _userManager.FindByNameAsync(loginModel.UserName);
                if (user != null)
                {
                    await _singInManager.SignOutAsync();
                    return (await _singInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<RegisterResultViewModel> Create(RegisterViewModel registerModel)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    // TODO: Remove hard coded data
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
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Phone = registerModel.Phone,
//                        Birthdate = registerModel.Birthdate
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

                    User user = new User
                    {
                        UserName = registerModel.Username,
                        Email = registerModel.Email,
                        IsActive = true,
                        UserInfo = userInfo,
                        Player = player
                    };

                    IdentityResult result
                        = await _userManager.CreateAsync(user, registerModel.Password);

                    if (!result.Succeeded)
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        transaction.Commit();
                        await _userManager.AddToRoleAsync(user, "Default");
                    }

                    return new RegisterResultViewModel
                    {
                        Result = result,
                        User = user
                    };
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Edit(UserDetailsViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDetailsViewModel entity)
        {
            throw new NotImplementedException();
        }

        public void Deactivate(string id)
        {
            throw new NotImplementedException();
        }
    }
}