﻿using System;
using System.Collections.Generic;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class GameLogic: IGameLogic
    {
        private readonly IGameRepository _gameRepository;

        public GameLogic(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        /// <summary>
        /// Get a game from the repository.
        /// </summary>
        /// <param name="gameId">The Id of the game to be retrieved.</param>
        /// <returns>The retrieved game.</returns>
        public Game GetGameById(int gameId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get games from the repository.
        /// </summary>
        /// <returns>The retrieved games as a list.</returns>
        public List<Game> List()
        {
            try
            {
                return _gameRepository.List();
            }
            catch (Exception)
            {
                throw new ConnectionException();
            }
        }

        /// <summary>
        /// Call the repository to create a new game.
        /// </summary>
        /// <param name="gameLength">The length of the game.</param>
        /// <param name="winningNumbers">The winning numbers of the game.</param>
        public void Create(int gameLength, string winningNumbers)
        {
            try
            {
                _gameRepository.Create(gameLength, winningNumbers);
            }
            catch (MySqlException)
            {
                throw new ConnectionException();
            }
        }
    }
}