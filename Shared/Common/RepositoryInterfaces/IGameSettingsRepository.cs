using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.GameSettings;

namespace Common.RepositoryInterfaces
{
    public interface IGameSettingsRepository
    {
        GameSettings GetById(int id);
        List<GameSettings> List();
        void Create(CreateGameSetting entity);
        void Update(UpdateGameSetting entity);
        void Delete(GameSettings entity);
    }
}