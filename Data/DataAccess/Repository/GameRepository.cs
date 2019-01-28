using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers.Exceptions;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using MySql.Data.MySqlClient;

namespace DataAccess.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly EntityContext _entityContext;

        public GameRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        /// <summary>
        /// Get game by Id.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <returns>The retrieved game.</returns>
        public Game GetById(int id)
        {
            try
            {
                Game game = _entityContext.Games.FirstOrDefault(g => g.GameId == id);

                if (game == null)
                    throw new NotFoundException("No game found");

                return game;
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all the games from the database.
        /// </summary>
        /// <returns>The retrieved games as a list.</returns>
        public List<Game> List()
        {
            try
            {
                List<Game> games = _entityContext.Games.ToList();

                if (!games.Any())
                    throw new NotFoundException();

                return games;
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Persists a new game to the database.
        /// </summary>
        /// <param name="gameLength">The length of the game; determines the endTime of the game.</param>
        /// <param name="winningNumbers">The winning numbers of the game generated randomly.</param>
        /// <returns>The persisted object.</returns>
        public void Create(int gameLength, string winningNumbers)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    GameInfo gameInfo = new GameInfo
                    {
                        CreatedAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow,
                        WinningNumbers = winningNumbers
                    };

                    Game game = new Game
                    {
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddMinutes(gameLength),
                        GameInfo = gameInfo
                    };

                    _entityContext.Add(game);
                    _entityContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Game entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Game entity)
        {
            throw new System.NotImplementedException();
        }
    }
}