using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Extensions;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EntityContext _entityContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(EntityContext entityContext, UserManager<User> userManager,
            SignInManager<User> singInManager, IPasswordValidator<User> passwordValidator,
            IPasswordHasher<User> passwordHasher)
        {
            _entityContext = entityContext;
            _userManager = userManager;
            _singInManager = singInManager;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal user)
        {
            try
            {
                var profile = await _userManager.GetUserAsync(user);

                if (profile == null)
                    throw new NullReferenceException();

                return profile;
            }
            catch (Exception)
            {
                throw;
            }
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
            catch (Exception)
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
            catch (Exception)
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
                    City city = new City
                    {
                        Name = registerModel.City
                    };

                    Address address = new Address
                    {
                        City = city,
                        Street = registerModel.Street,
                        ZipCode = registerModel.ZipCode,
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    };

                    UserInfo userInfo = new UserInfo
                    {
                        Address = address,
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Phone = registerModel.Phone,
                        Birthdate = DateTime.Parse(registerModel.Birthdate)
                };

                    Player player = new Player
                    {
                        Balance = 5,
                        NumberOfGamesPlayed = 0,
                        NumberOfGamesWon = 0,
                        TotalSpent = 0,
                        UpdatedAt = DateTime.UtcNow,
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
                        transaction.Rollback();
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Default");
                        transaction.Commit();
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

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    User user = await _userManager.FindByIdAsync(model.Id.ToString());

                    if (user != null)
                    {
                        IdentityResult validPass = null;
                        if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.ReTypePassword) &&
                            model.Password.Equals(model.ReTypePassword))
                        {
                            validPass = await _passwordValidator.ValidateAsync(_userManager,
                                user, model.Password);

                            if (validPass.Succeeded)
                                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                            else
                                throw new ArgumentException("Please provide the correct password");
                        }
                        else
                        {
                            throw new FormatException("Please provide the correct password");
                        }

                        if (string.IsNullOrEmpty(model.Password) && !validPass.Succeeded)
                            throw new FormatException("The password provided is not valid");

                        IdentityResult result = await _userManager.UpdateAsync(user);

                        if (!result.Succeeded)
                            transaction.Rollback();
                        else
                            transaction.Commit();

                        return result;
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(UserDetailsViewModel entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    

//                    _entityContext.Update(gameSetting);
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

        public void Deactivate(string id)
        {
            // TODO: Deactivate account
            throw new NotImplementedException();
        }
    }
}