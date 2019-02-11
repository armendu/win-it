using System;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class PlayerLogic: IPlayerLogic
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerLogic(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Player GetById(int playerId)
        {
            try
            {
                return _playerRepository.GetById(playerId);
            }
            catch (NullReferenceException)
            {
                throw new NotFoundException("Player was not found");
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }

        public void Update(int id)
        {
            try
            {
                _playerRepository.Update(id);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }
    }
}