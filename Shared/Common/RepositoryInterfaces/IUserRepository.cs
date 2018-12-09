using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels;

namespace Common.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetById(string id);
        List<UserDetailsViewModel> List();
        void Add(UserDetailsViewModel entity);
        void Update(UserDetailsViewModel entity);
        void Delete(UserDetailsViewModel entity);
    }
}