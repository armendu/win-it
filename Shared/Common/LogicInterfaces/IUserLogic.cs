using System.Collections.Generic;
using Entities.ViewModels;
using Entities.ViewModels.User;

namespace Common.LogicInterfaces
{
    public interface IUserLogic
    {
        UserDetailsViewModel GetUserById(int profileID);

        List<UserDetailsViewModel> List();

        void Create(RegisterViewModel registerModel);

        bool Login();
    }
}