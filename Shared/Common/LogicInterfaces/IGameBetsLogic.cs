using Entities.ViewModels.Game;

namespace Common.LogicInterfaces
{
    public interface IGameBetsLogic
    {
        void Create(CreateGameBetViewModel model);
    }
}