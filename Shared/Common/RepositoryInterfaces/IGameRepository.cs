using System.Collections.Generic;
using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface IGameRepository
    {
        Game GetRunningGame();
        Game GetById(int id);
        List<Game> List();
        void Create(int gameLength, string winningNumbers, decimal winningPot);
        void Update(Game model);
        void UpdatePot(Game model, decimal sum, ref decimal totalPot);
    }
}