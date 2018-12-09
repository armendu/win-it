using System.Collections.Generic;
using Entities.ViewModels;

namespace Common.LogicInterfaces
{
    public interface IUserLogic
    {
        UserDetailsViewModel GetUserById(int profileID);

        List<UserDetailsViewModel> GetUsersList();

        void RegisterUser();
    }
}