using System;
using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Game;

namespace Common.LogicInterfaces
{
    public interface IGameLogic
    {
        Game GetGameById(int gameId);

        List<Game> List();

        void Create(int gameLength, string winningNumbers);

        void CreateGameBet(CreateGameBetViewModel entity);
    }
}