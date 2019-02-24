using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Game;

namespace Common.RepositoryInterfaces
{
    public interface IGameBetsRepository
    {
        List<GameBet> GetGameBetsForPlayer(int playerId);
        void Create(CreateGameBetViewModel model);
    }
}