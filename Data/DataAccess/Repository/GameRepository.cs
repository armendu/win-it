using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.Game;
using Entities.ViewModels.Transaction;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace DataAccess.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly EntityContext _entityContext;
        private readonly ITransactionLogic _transactionLogic;

        public GameRepository(EntityContext entityContext, ITransactionLogic transactionLogic)
        {
            _entityContext = entityContext;
            _transactionLogic = transactionLogic;
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
                    throw new NotFoundException("No model found");

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
                        UpdateAt = DateTime.UtcNow,
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
        /// Persists a new bet.
        /// </summary>
        /// <param name="model">The length of the model; determines the endTime of the model.</param>
        /// <returns>The persisted object.</returns>
        public void CreateGameBet(CreateGameBetViewModel model)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    CreateTransactionViewModel betTransaction = new CreateTransactionViewModel
                    {
                        GameId = model.GameId,
                        PlayerId = model.PlayerId,
                        Sum = model.Sum
                    };

                    var transactionModel = _transactionLogic.Create(betTransaction);

                    GameBet bet = new GameBet
                    {
                        GameId = model.GameId,
                        PlayerId = model.PlayerId,
                        ChosenNumbers = JsonConvert.SerializeObject(model.Numbers),
                        CreatedAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };
                    
                    _entityContext.Add(bet);
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
            // TODO: THIS IS WHERE WE CHOOSE THE WINNERS.
            Game game =
                _entityContext.Games.FirstOrDefault(x => x.GameId == entity.GameId);

            if (game != null)
            {
                game.GameProcessed = true;
                game.GameInfo.UpdateAt = DateTime.UtcNow;

                _entityContext.Update(game);
                _entityContext.SaveChanges();
            }
            else
                throw new NullReferenceException();
        }

        public void Delete(Game entity)
        {
            throw new System.NotImplementedException();
        }
    }
}