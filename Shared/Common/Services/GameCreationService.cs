using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Helpers.Exceptions;
using Common.LogicInterfaces;
using Entities.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Services
{
    public class GameCreationService : AutomaticService, IHostedService, IDisposable
    {
        private readonly IGameLogic _gameLogic;
        private readonly IGameSettingsLogic _gameSettingsLogic;
        private readonly ILogger _logger;
        private Timer _startGameTimer;
        private static readonly Random random = new Random();

        public GameCreationService(IGameLogic gameLogic, IGameSettingsLogic gameSettingsLogic,
            ILogger<GameCreationService> logger)
        {
            _gameLogic = gameLogic;
            _gameSettingsLogic = gameSettingsLogic;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            // Set the default gameLength
            int gameLength = 60;
            decimal winningPot = 5;

            (int, decimal) gameInformation = (gameLength, winningPot);

            try
            {
                GameSettings gameSettings = _gameSettingsLogic.List().FirstOrDefault();

                if (gameSettings != null)
                {
                    if (gameSettings.GameLength > 0 && Math.Abs(gameSettings.WinningPot) > 0)
                        gameInformation.Item1 = gameSettings.GameLength;

                    if (Math.Abs(gameSettings.WinningPot) > 0)
                        gameInformation.Item2 = gameSettings.WinningPot;
                }
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Error: {ex.Message} @ {GetType().Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred: {ex.Message} @ {GetType().Name}");
            }

            _startGameTimer = new Timer(StartGame, gameInformation, TimeSpan.Zero,
                TimeSpan.FromSeconds((gameLength + 60) * 60));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        /// <param name="state">The length of the game.</param>
        protected void StartGame(object state)
        {
            string winningNumbers = GenerateWinningNumbers();

            try
            {
                var gameInformationTuple = ((int, decimal)) state;

                _gameLogic.Create(gameInformationTuple.Item1, winningNumbers, gameInformationTuple.Item2);

                _logger.LogInformation(
                    $"A new game was created with the following winning numbers: {winningNumbers} @ {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred: {ex.Message} @ {GetType().Name}");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Timed Background Service is stopping.");

            _startGameTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _startGameTimer?.Dispose();
        }

        /// <summary>
        /// Generates the winning numbers.
        /// </summary>
        /// <returns>The winning numbers in JSON format.</returns>
        protected virtual string GenerateWinningNumbers()
        {
            List<int> winningNumbers = new List<int>();

            while (winningNumbers.Count < 7)
            {
                int generatedNumber = random.Next(1, 40);

                if (winningNumbers.Contains(generatedNumber))
                    continue;

                winningNumbers.Add(generatedNumber);
            }

            return JsonConvert.SerializeObject(winningNumbers);
        }
    }
}