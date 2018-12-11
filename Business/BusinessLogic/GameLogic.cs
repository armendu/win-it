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

        public Game GetGameById(int gameId)
        {
            throw new System.NotImplementedException();
        }

        public List<Game> GetGames()
        {
            throw new System.NotImplementedException();
        }

        public void CreateGame()
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