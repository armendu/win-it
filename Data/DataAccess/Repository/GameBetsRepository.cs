using System;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.Game;
using Newtonsoft.Json;

namespace DataAccess.Repository
{
    public class GameBetsRepository: IGameBetsRepository
    {
        private readonly EntityContext _entityContext;
        private readonly IPlayerLogic _playerLogic;

        public GameBetsRepository(EntityContext entityContext, IPlayerLogic playerLogic)
        {
            _entityContext = entityContext;
            _playerLogic = playerLogic;
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