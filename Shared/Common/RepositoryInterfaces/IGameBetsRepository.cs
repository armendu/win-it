using Entities.ViewModels.Game;

namespace Common.RepositoryInterfaces
{
    public interface IGameBetsRepository
    {
        void Create(CreateGameBetViewModel model);
    }
}