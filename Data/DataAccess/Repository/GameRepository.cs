using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels;
using Newtonsoft.Json;

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

        public Game GetRunningGame()
        {
            try
            {
                Game game = _entityContext.Games.FirstOrDefault(g => g.GameProcessed == false);

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
        public void Create(int gameLength, string winningNumbers, decimal winningPot)
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
                        WinningPot = winningPot,
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
        /// <param name="model"></param>
        public void Update(Game model)
        {
            try
            {
                // Get all game bets of the running game.
                List<GameBet> gameBetsForGame = _entityContext.GameBets.Where(g => g.GameId == model.GameId).ToList();
                List<GameWinner> winners = new List<GameWinner>();
                List<Player> playersThatWon = new List<Player>();
                decimal sumToBeAddedToBalance = 0;

                if (gameBetsForGame.Any())
                {
                    // Get the winners of the game.
                    List<WinnerInformation> listOfWinners = FindWinners(model, gameBetsForGame);

                    if (listOfWinners.Any())
                    {
                        // Check the sum that was invested and confirm wins.
                        sumToBeAddedToBalance = ConfirmWinners(listOfWinners.OrderBy(o => o.SumInvested).ToList(),
                            model.GameInfo.WinningPot ?? 0);

                        foreach (var winner in listOfWinners)
                        {
                            winners.Add(winner.GameWinnerInformation);
                            playersThatWon.Add(_entityContext.Players.FirstOrDefault(p =>
                                p.PlayerId == winner.GameWinnerInformation.PlayerId));
                        }
                    }

                    foreach (var gameBet in gameBetsForGame)
                    {
                        gameBet.BetStatus =
                            listOfWinners.FirstOrDefault(w =>
                                w.GameWinnerInformation.GameId == gameBet.GameId &&
                                w.GameWinnerInformation.PlayerId == gameBet.PlayerId) != null
                                ? BetStatus.Won
                                : BetStatus.Lost;
                    }
                }

                if (winners.Any())
                {
                    foreach (var player in playersThatWon)
                    {
                        player.NumberOfGamesWon += 1;
                        player.Balance += sumToBeAddedToBalance;
                    }

                    _entityContext.UpdateRange(gameBetsForGame);

                    _entityContext.UpdateRange(playersThatWon);
                    _entityContext.AddRange(winners);
                }

                model.GameProcessed = true;
                model.GameInfo.UpdatedAt = DateTime.UtcNow;

                _entityContext.Update(model);
                _entityContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePot(Game model, decimal sum, ref decimal totalPot)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    Game game = _entityContext.Games.FirstOrDefault(g => g.GameId == model.GameId);

                    if (game == null)
                        throw new NullReferenceException();

                    game.GameInfo.WinningPot += sum;
                    game.GameInfo.UpdatedAt = DateTime.UtcNow;
                    totalPot = game.GameInfo.WinningPot ?? 0;

                    _entityContext.Update(game);
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

        private List<WinnerInformation> FindWinners(Game game, List<GameBet> gameBets)
        {
            // COMMENT
            List<int> winningNumbers = JsonConvert.DeserializeObject<List<int>>(game.GameInfo.WinningNumbers);
            List<WinnerInformation> winners = new List<WinnerInformation>();
            List<int> gameBetNumbers = new List<int>(7);

            foreach (var bet in gameBets)
            {
                gameBetNumbers = JsonConvert.DeserializeObject<List<int>>(bet.ChosenNumbers);

                // Check how many numbers
                if (winningNumbers.ArePermutations(gameBetNumbers))
                {
                    winners.Add(new WinnerInformation
                    {
                        GameWinnerInformation = new GameWinner
                        {
                            GameId = game.GameId,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            PlayerId = bet.PlayerId,
                            Player = bet.Player,
                            Game = game,
                            NumbersMatches = 7
                        },
                        SumInvested = bet.Transaction.Sum
                    });
                }
            }

            return winners;
        }

        private decimal ConfirmWinners(List<WinnerInformation> listOfWinners, decimal winningPot)
        {
            if (winningPot > 0)
            {
                // Set aside 10% for the house
                decimal sumToBeGiven = (winningPot - (winningPot * (decimal) 0.10)) / listOfWinners.Count;

                foreach (var winner in listOfWinners)
                {
                    winner.GameWinnerInformation.SumWon = sumToBeGiven;
                }

                return sumToBeGiven;
            }

            return 0;
        }
    }
}