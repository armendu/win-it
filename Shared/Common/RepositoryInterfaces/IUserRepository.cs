using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Entities.ViewModels.User;

namespace Common.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> FindById(string id);
        List<User> List();
        Task<bool> Login(LoginViewModel loginModel);
        Task<RegisterResultViewModel> Create(RegisterViewModel registerModel);
        void Edit(UserDetailsViewModel entity);
        void Deactivate(string id);
    }
}