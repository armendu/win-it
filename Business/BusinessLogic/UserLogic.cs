using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class UserLogic: IUserLogic
    {
        private readonly IUserRepository _userRepository;
        
        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal user)
        {
            try
            {
                return await _userRepository.GetCurrentUser(user);
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

        public async Task<User> FindById(string id)
        {
            try
            {
                return await _userRepository.FindById(id);
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

        public List<User> List()
        {
            try
            {
               return _userRepository.List();
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<RegisterResultViewModel> Create(RegisterViewModel model)
        {
            try
            {
                return await _userRepository.Create(model);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<bool> Login(LoginViewModel loginModel)
        {
            try
            {
                return await _userRepository.Login(loginModel);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                return await _userRepository.ChangePassword(model);
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
//
//        public void ChangeAccountStatus(int profileID, bool status)
//        {
//            try
//            {
//                _userRepository.ChangeAccountStatus(profileID, status);
//            }
//            catch (NullReferenceException)
//            {
//                throw new NotFoundException("User was not found");
//            }
//            catch (Exception)
//            {
//                throw new OperationException();
//            }
//        }
    }
}