using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly EntityContext _entityContext;
        private readonly ITransactionLogic _transactionLogic;
        private readonly IPlayerLogic _playerLogic;

        public GameRepository(EntityContext entityContext, ITransactionLogic transactionLogic, IPlayerLogic playerLogic)
        {
            _entityContext = entityContext;
            _transactionLogic = transactionLogic;
            _playerLogic = playerLogic;
        }

        /// <summary>
        /// Get model by Id.
        /// </summary>
        /// <param name="id">The id of the model.</param>
        /// <returns>The retrieved model.</returns>
        /// <exception cref="NotFoundException">If the game is not found.</exception>
        public Game GetById(int id)
        {
            try
            {
                Game game = _entityContext.Games.FirstOrDefault(g => g.GameId == id);

                if (game == null)
                    throw new NullReferenceException();

                return game;
            }
            catch (Exception)
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
                    throw new NullReferenceException();

                return games;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Persists a new model to the database.
        /// </summary>
        /// <param name="gameLength">The length of the model; determines the endTime of the model.</param>
        /// <param name="winningNumbers">The winning numbers of the model generated randomly.</param>
        /// <returns>The persisted object.</returns>
        public void Create(int gameLength, string winningNumbers)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    List<Game> games = _entityContext.Games.Where(g => g.GameProcessed == false).ToList();

                    foreach (var element in games)
                    {
                        if (!element.GameProcessed)
                        {
                            Update(element);
                        }
                    }

                    GameInfo gameInfo = new GameInfo
                    {
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        WinningNumbers = winningNumbers
                    };

                    Game game = new Game
                    {
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddMinutes(gameLength),
                        GameInfo = gameInfo,
                        GameProcessed = false
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

        /// <summary>
        /// Updates a running game.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Game entity)
        {
            try
            {
                // TODO: THIS IS WHERE WE CHOOSE THE WINNERS.
                Game game =
                    _entityContext.Games.FirstOrDefault(x => x.GameId == entity.GameId);

                if (game == null)
                    throw new OperationException("Game not found!");

                game.GameProcessed = true;
                game.GameInfo.UpdatedAt = DateTime.UtcNow;

                _entityContext.Update(game);
                _entityContext.SaveChanges();
                    
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}