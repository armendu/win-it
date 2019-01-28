using System;
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
    public class GameCreationService : IHostedService, IDisposable
    {
        private readonly IGameLogic _gameLogic;
        private readonly IGameSettingsLogic _gameSettingsLogic;
        private readonly ILogger _logger;
        private Timer _startGameTimer;
        private Timer _endGameTimer;
        private static readonly Random random = new Random();

        public GameCreationService(IGameLogic gameLogic, IGameSettingsLogic gameSettingsLogic,
            ILogger<GameCreationService> logger)
        {
            _gameLogic = gameLogic;
            _gameSettingsLogic = gameSettingsLogic;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            // Set the default gameLength
            int gameLength = 60;

            try
            {
                GameSettings gameSettings = _gameSettingsLogic.List().FirstOrDefault();

                if (gameSettings != null && gameSettings.GameLength > 0)
                    gameLength = gameSettings.GameLength;
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Error: {ex.Message} @ {GetType().Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred: {ex.Message} @ {GetType().Name}");
            }

            _startGameTimer = new Timer(StartGame, gameLength, TimeSpan.Zero,
                TimeSpan.FromSeconds((gameLength + 60)));
            
            _endGameTimer = new Timer(EndGame, string.Empty, TimeSpan.FromSeconds(gameLength + 60), TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        /// <param name="state">The length of the game.</param>
        private void StartGame(object state)
        {
            _logger.LogInformation($"Starting a game from the Hosted Service @ {DateTime.UtcNow}");

            // TODO: Create method for generating random numbers.
            string winningNumbers = GenerateWinningNumbers();

            try
            {
                _gameLogic.Create((int) state, winningNumbers);
                _logger.LogInformation($"A new game was created with the following winning numbers: {winningNumbers}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred: {ex.Message} @ {GetType().Name}");
            }
        }

        /// <summary>
        /// Ends a running game, and chooses the winners.
        /// </summary>
        /// <param name="state"></param>
        private void EndGame(object state)
        {
            var now = DateTime.UtcNow;
            // Finish up a game and find the winners.
            _logger.LogInformation($"The end timer did start @ {now}");

            try
            {
                DateTime endTime = _gameLogic.List().LastOrDefault().EndTime;

                if (endTime < now)
                {
                    _logger.LogInformation($"Game has finished");
                }
                else
                {
                    _logger.LogInformation($"Game has finished");
                }
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError($"Game was not found. Error message: {ex}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The following error occurred: {ex.Message} @ {GetType().Name}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

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
        private string GenerateWinningNumbers()
        {
            int[] winningNumbers = new[]
            {
                random.Next(1, 39),
                random.Next(1, 39),
                random.Next(1, 39),
                random.Next(1, 39),
                random.Next(1, 39),
                random.Next(1, 39),
                random.Next(1, 39)
            };

            return JsonConvert.SerializeObject(winningNumbers);
        }
    }
}