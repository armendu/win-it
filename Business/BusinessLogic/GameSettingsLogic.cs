using System;
using System.Collections.Generic;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using Entities.ViewModels.GameSettings;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class GameSettingsLogic : IGameSettingsLogic
    {
        private readonly IGameSettingsRepository _gameSettingsRepository;

        public GameSettingsLogic(IGameSettingsRepository gameSettingsRepository)
        {
            _gameSettingsRepository = gameSettingsRepository;
        }

        public GameSettings GetSettingsById(int settingId)
        {
            try
            {
                return _gameSettingsRepository.GetById(settingId);
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
                List<GameSettings> gameSettings = _gameSettingsRepository.List();
                return gameSettings;
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Create(CreateGameSetting entity)
        {
            try
            {
                _gameSettingsRepository.Create(entity);
            }
            catch (Exception)
            {
                throw new OperationException("An error occured during creating project!");
            }
        }

        public void Update(UpdateGameSetting entity)
        {
            try
            {
                _gameSettingsRepository.Update(entity);
            }
            catch (Exception)
            {
                throw new OperationException("An error occured during creating project!");
            }
        }

        public void Delete(GameSettings entity)
        {
            try
            {
                _gameSettingsRepository.Delete(entity);
            }
            catch (Exception)
            {
                throw new OperationException("An error occured during creating project!");
            }
        }
    }
}