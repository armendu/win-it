using System.Collections.Generic;
using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface IGameSettingsLogic
    {
        GameSettings GetSettingsById(int settingId);

        List<GameSettings> List();

        void Create();
    }
}