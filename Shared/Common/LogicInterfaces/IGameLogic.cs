﻿using System.Collections.Generic;
using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface IGameLogic
    {
        Game GetGameById(int gameId);

        List<Game> GetGames();

        void CreateGame();
    }
}