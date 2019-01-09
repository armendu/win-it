using System;
using System.Collections.Generic;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
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

        public UserDetailsViewModel GetUserById(int profileID)
        {
//            try
//            {
//                UserDetailsViewModel user = _userRepository.GetById(profileID);
//
//                return user;
//            }
//            catch (NullReferenceException)
//            {
//                throw
//                    new NotFoundException("User was not found");
//            }
//            catch (MySqlException)
//            {
//                throw new ConnectionException();
//            }
            throw new NotImplementedException();
        }

        public List<UserDetailsViewModel> List()
        {
            try
            {
                List<UserDetailsViewModel> list = _userRepository.List();

                return list;
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public void Create(RegisterViewModel model)
        {
            try
            {
                _userRepository.Create(model);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public bool Login()
        {
            return false;
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