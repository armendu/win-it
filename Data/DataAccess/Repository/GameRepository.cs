using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            try
            {
                List<Game> someGames = new List<Game>
                {
                    new Game
                    {
                        GameId = 1,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    },
                    new Game
                    {
                        GameId = 2,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    },
                    new Game
                    {
                        GameId = 3,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    },
                    new Game
                    {
                        GameId = 4,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    },
                    new Game
                    {
                        GameId = 5,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    },
                    new Game
                    {
                        GameId = 6,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    },
                    new Game
                    {
                        GameId = 7,
                        StartTime = DateTime.UtcNow,
                        EndTime =  DateTime.UtcNow
                    }
                };
                List<Game> games = _entityContext.Games.ToList();
                return someGames;
                //return games;
            }
            catch (SqlException)
            {
                throw;
            }
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