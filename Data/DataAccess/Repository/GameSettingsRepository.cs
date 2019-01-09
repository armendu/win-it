using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers.Exceptions;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;
using Entities.ViewModels.GameSettings;
using MySql.Data.MySqlClient;

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

                if (gameSetting == null)
                    throw new NotFoundException($"GameSetting with Id: {id} was not found!");

                return gameSetting;
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public List<GameSettings> List()
        {
            try
            {
                List<GameSettings> gameSettings = _entityContext.GameSettings.ToList();

                if (gameSettings.Count == 0)
                    throw new NotFoundException("There are no Game Settings to be shown!");

                return gameSettings;
            }
            catch (MySqlException)
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
                    GameSettings gameSettings =
                        _entityContext.GameSettings.FirstOrDefault(x => x.GameSettingId == entity.GameSettingId);

                    if (gameSettings != null)
                    {
                        _entityContext.Remove(gameSettings);
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