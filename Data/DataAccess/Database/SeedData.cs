using System;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Database
{
    public static class SeedData
    {
        /// <summary>
        /// Ensures that there are initial Game Settings saved in the database.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void EnsurePopulated(IApplicationBuilder app, IConfiguration configuration)
        {
            EntityContext context = app.ApplicationServices
                .GetRequiredService<EntityContext>();
            context.Database.Migrate();
            if (!context.GameSettings.Any())
            {
                bool correctGameLength =
                    int.TryParse(configuration["InitialGameSettings:GameLength"], out int gameLength);
                bool correctWinningPot =
                    int.TryParse(configuration["InitialGameSettings:WinningPot"], out int winningPot);

                context.GameSettings.Add(new GameSettings
                {
                    GameSettingId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    GameLength = correctGameLength ? gameLength : 60,
                    WinningPot = correctWinningPot ? winningPot : 10
                });
            }

            context.SaveChanges();
        }
    }
}