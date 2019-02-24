using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Game;

namespace Common.LogicInterfaces
{
    public interface IGameBetsLogic
    {
        List<GameBet> GetGameBetsForPlayer(int playerId);
        void Create(CreateGameBetViewModel model);
    }
}