using System;
using System.Collections.Generic;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;

namespace DataAccess.Repository
{
    public class GameRepository: IGameRepository
    {
        private readonly EntityContext _entityContext;

        public GameRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public Game GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<Game> List()
        {
            throw new System.NotImplementedException();
        }

        public Game Create(Game entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    Game game = new Game
                    {
                        StartTime = DateTime.UtcNow
                    };

                    _entityContext.Add(game);
                    _entityContext.SaveChanges();

                    transaction.Commit();
                    return game;
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