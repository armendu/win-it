using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.GameSettings;

namespace Common.LogicInterfaces
{
    public interface IGameSettingsLogic
    {
        GameSettings GetSettingsById(int settingId);

        List<GameSettings> List();

        void Create(CreateGameSetting entity);

        void Update(UpdateGameSetting entity);

        void Delete(GameSettings entity);
    }
}