using System.Collections.Generic;
using Entities.ViewModels;

namespace Common.RepositoryInterfaces
{
    public interface IUserRepository
    {
        UserDetailsViewModel GetById(int id);
        List<UserDetailsViewModel> List();
        UserDetailsViewModel Add(UserDetailsViewModel entity);
        void Update(UserDetailsViewModel entity);
        void Delete(UserDetailsViewModel entity);
    }
}