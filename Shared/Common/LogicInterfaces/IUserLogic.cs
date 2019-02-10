using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace Common.LogicInterfaces
{
    public interface IUserLogic
    {
        Task<User> GetCurrentUser(ClaimsPrincipal user);

        Task<User> FindById(string id);

        List<User> List();

        Task<RegisterResultViewModel> Create(RegisterViewModel registerModel);

        Task<bool> Login(LoginViewModel loginModel);

        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);
    }
}