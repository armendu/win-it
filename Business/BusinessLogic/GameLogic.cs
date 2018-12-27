using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;

namespace BusinessLogic
{
    public class GameLogic: IGameLogic
    {
        private readonly IGameRepository _gameRepository;

        public GameLogic(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        /// <summary>
        /// Get a game from the repository.
        /// </summary>
        /// <param name="gameId">The Id of the game to be retrieved.</param>
        /// <returns>The retrieved game.</returns>
        public Game GetGameById(int gameId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get games from the repository.
        /// </summary>
        /// <returns>The retrieved games as a list.</returns>
        public List<Game> List()
        {
            try
            {
                return _gameRepository.List();
            }
            catch (Exception)
            {
                throw new ConnectionException();
            }
        }

        public void Create()
        {
            try
            {
                Game model = new Game();
                _gameRepository.Create(model);
            }
            catch (SqlException)
            {
                throw new ConnectionException();
            }
        }
    }
}