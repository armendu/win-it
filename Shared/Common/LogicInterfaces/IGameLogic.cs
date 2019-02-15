using System.Collections.Generic;
using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface IGameLogic
    {
        Game GetRunningGame();
        Game GetGameById(int gameId);

        List<Game> List();

        void Create(int gameLength, string winningNumbers, decimal winningPot);

        void UpdatePot(Game model, decimal sum, ref decimal totalPot);
    }
}