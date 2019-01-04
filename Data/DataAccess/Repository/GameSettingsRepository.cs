using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.GameSettings;

namespace DataAccess.Repository
{
    public class GameSettingsRepository : IGameSettingsRepository
    {
        private readonly EntityContext _entityContext;

        public GameSettingsRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public GameSettings GetById(int id)
        {
            try
            {
                GameSettings gameSetting = _entityContext.GameSettings.FirstOrDefault(g => g.GameSettingId == id);

                return gameSetting;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<GameSettings> List()
        {
            try
            {
                List<GameSettings> gameSettings = _entityContext.GameSettings.ToList();

                if (gameSettings.Count > 0)
                    return gameSettings;

                throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void Create(CreateGameSetting entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    GameSettings gameSettings = new GameSettings
                    {
                        GameLength = entity.GameLength,
                        CreatedAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };

                    _entityContext.GameSettings.Add(gameSettings);
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

        public void Update(UpdateGameSetting entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    GameSettings gameSetting =
                        _entityContext.GameSettings.FirstOrDefault(x => x.GameSettingId == entity.GameSettingId);

                    if (gameSetting != null)
                    {
                        gameSetting.GameLength = entity.GameLength;
                        gameSetting.UpdateAt = DateTime.UtcNow;

                        _entityContext.Update(gameSetting);
                        _entityContext.SaveChanges();
                        transaction.Commit();
                    }
                    else
                        throw new NullReferenceException();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(GameSettings entity)
        {
            using (var transaction = _entityContext.Database.BeginTransaction())
            {
                try
                {
                    GameSettings project =
                        _entityContext.GameSettings.FirstOrDefault(x => x.GameSettingId == entity.GameSettingId);

                    if (project != null)
                    {
                        _entityContext.Remove(project);
                        _entityContext.SaveChanges();
                        transaction.Commit();
                    }
                    else
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