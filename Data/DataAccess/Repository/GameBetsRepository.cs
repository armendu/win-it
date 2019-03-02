using System;
using System.Collections.Generic;
using System.Linq;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.Game;
using Newtonsoft.Json;

namespace DataAccess.Repository
{
    public class GameBetsRepository : IGameBetsRepository
    {
        private readonly EntityContext _entityContext;
        private readonly IPlayerLogic _playerLogic;

        public GameBetsRepository(EntityContext entityContext, IPlayerLogic playerLogic)
        {
            _entityContext = entityContext;
            _playerLogic = playerLogic;
        }

        public List<GameBet> GetGameBetsForPlayer(int playerId)
        {
            try
            {
                List<GameBet> gameBets = _entityContext.GameBets.Where(g => g.PlayerId == playerId).ToList();

                if (gameBets.Count == 0)
                    throw new NullReferenceException();

                return gameBets;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Create(CreateGameBetViewModel model)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    Player player = _playerLogic.GetById(model.PlayerId);

                    if (player.Balance < model.Sum)
                        throw new ArgumentException("The player's balance is smaller and the sum provided");

                    player.Balance -= model.Sum;
                    player.NumberOfGamesPlayed += 1;
                    player.TotalSpent += model.Sum;

                    Transaction transactionModel = new Transaction
                    {
                        Sum = model.Sum,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Player = player
                    };

                    GameBet bet = new GameBet
                    {
                        GameId = model.GameId,
                        PlayerId = model.PlayerId,
                        ChosenNumbers = JsonConvert.SerializeObject(model.Numbers),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Transaction = transactionModel
                    };

                    _entityContext.Update(player);
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
    }
}