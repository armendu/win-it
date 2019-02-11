using System;
using System.Collections.Generic;
using Common.Helpers.Exceptions;
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
            catch (NullReferenceException)
            {
                throw new NotFoundException("GameSettings was not found!");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public List<GameSettings> List()
        {
            try
            {
                List<GameSettings> gameSettings = _gameSettingsRepository.List();
                return gameSettings;
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("GameSettings were not found!");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public void Create(CreateGameSetting entity)
        {
            try
            {
                _gameSettingsRepository.Create(entity);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
            catch (Exception)
            {
                throw new OperationException("An error occured while creating new Game Settings!");
            }
        }

        public void Update(UpdateGameSetting entity)
        {
            try
            {
                _gameSettingsRepository.Update(entity);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
            catch (Exception)
            {
                throw new OperationException("An error occured while updating Game Settings!");
            }
        }
    }
}