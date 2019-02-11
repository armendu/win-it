using System;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.ViewModels.Game;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class GameBetsLogic: IGameBetsLogic
    {
        private readonly IGameBetsRepository _gameBetsRepository;

        public GameBetsLogic(IGameBetsRepository gameBetsRepository)
        {
            _gameBetsRepository = gameBetsRepository;
        }

        public void Create(CreateGameBetViewModel model)
        {
            try
            {
                _gameBetsRepository.Create(model);
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
    }
}