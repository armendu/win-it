using System;
using System.Collections.Generic;
using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface IGameLogic
    {
        Game GetGameById(int gameId);

        List<Game> List();

        void Create(int gameLength, string winningNumbers);
    }
}