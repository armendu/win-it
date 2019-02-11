using System;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;

namespace DataAccess.Repository
{
    public class PlayerRepository: IPlayerRepository
    {
        private readonly EntityContext _entityContext;

        public PlayerRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public Player GetById(int id)
        {
            try
            {
                var player = _entityContext.Players.FirstOrDefault(p => p.PlayerId == id);

                if (player == null)
                    throw new NullReferenceException();

                return player;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(int id)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
//                    GameSettings gameSetting =
//                        _entityContext.GameSettings.FirstOrDefault(x => x.GameSettingId == entity.GameSettingId);
//
//                    if (gameSetting != null)
//                    {
//                        gameSetting.GameLength = entity.GameLength;
//                        gameSetting.UpdatedAt = DateTime.UtcNow;
//
//                        _entityContext.Update(gameSetting);
//                        _entityContext.SaveChanges();
//                        transaction.Commit();
//                    }
//                    else
                        throw new NullReferenceException();
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