using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Common.Interfaces;
using DataAccess.Database;
using Entities.Entities;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository: IRepository<UserDetailsViewModel>
    {
        private readonly EntityContext _entityContext;

        public UserRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public UserDetailsViewModel GetById(int id)
        {
            UserInfo profile = _entityContext.UserInfos.FirstOrDefault(u => u.UserInfoID.Equals(id));

            if (profile == null)
            {
                throw new NullReferenceException();
            }

            Address address = profile.Address;
            AddressViewModel addressModel = new AddressViewModel()
            {
                Street = address.Street,
                ZipCode = address.Street
            };

            UserDetailsViewModel userModel = new UserDetailsViewModel()
            {
                ProfileID = profile.UserInfoID,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Birthdate = profile.Birthdate,
                LoginEmail = profile.User.Email,
                Address = addressModel,
                Phone = profile.Phone,
                DateRegistered = profile.User.CreatedAt,
                Role = profile.User.Role.Name
            };

            return userModel;
        }

        public List<UserDetailsViewModel> List()
        {
            try
            {
                List<UserDetailsViewModel> allUsers = _entityContext.UserInfos.Where(u => u.User.IsActive)
                    .Select(t => new UserDetailsViewModel()
                    {
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        ProfileID = t.UserInfoID,
                    })
                    .OrderBy(t => t.FirstName)
                    .ThenBy(t => t.LastName)
                    .ToList();

                if (allUsers.IsNullOrEmpty())
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

        public UserDetailsViewModel Add(UserDetailsViewModel entity)
        {
            throw new NotImplementedException();
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