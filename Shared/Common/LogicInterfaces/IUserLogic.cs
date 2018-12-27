using System.Collections.Generic;
using Entities.ViewModels;

namespace Common.LogicInterfaces
{
    public interface IUserLogic
    {
        UserDetailsViewModel GetUserById(int profileID);

        List<UserDetailsViewModel> List();

        void Create();

        bool Login();
    }
}