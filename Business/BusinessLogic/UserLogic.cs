using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.User;
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
//
//        public void EditUser(EditUser model)
//        {
//            try
//            {
//                _userRepository.EditUser(model);
//            }
//            catch (NullReferenceException)
//            {
//                throw new
//                    NotFoundException("User was not found");
//            }
//            catch (Exception)
//            {
//                throw new
//                    OperationException("An error occurred while updating User's profile");
//            }
//        }
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
//
//        public int GetProfileID(string userID)
//        {
//            try
//            {
//                if (String.IsNullOrEmpty(userID))
//                    throw new OperationException("User was not specified.");
//
//                int profileID = _userRepository.GetProfileID(userID);
//                return profileID;
//            }
//            catch (MySqlException)
//            {
//                throw new ConnectionException();
//            }
//            catch (NullReferenceException)
//            {
//                throw new NotFoundException("User was not found");
//            }
//        }
    }
}