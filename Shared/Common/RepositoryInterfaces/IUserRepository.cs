using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace Common.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> GetCurrentUser(ClaimsPrincipal user);
        Task<User> FindById(string id);
        List<User> List();
        Task<bool> Login(LoginViewModel loginModel);
        Task<RegisterResultViewModel> Create(RegisterViewModel registerModel);
        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);
        void Deactivate(string id);
    }
}