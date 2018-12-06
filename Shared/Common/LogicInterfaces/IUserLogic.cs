using System.Collections.Generic;
using Entities.ViewModels;

namespace Common.LogicInterfaces
{
    public interface IUserLogic
    {
        UserDetailsViewModel GetUserByID(int profileID);

        List<UserDetailsViewModel> GetUsersList();
    }
}