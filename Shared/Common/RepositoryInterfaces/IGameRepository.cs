using System.Collections.Generic;
using Entities.Models;
using Entities.ViewModels.Game;

namespace Common.RepositoryInterfaces
{
    public interface IGameRepository
    {
        Game GetById(int id);
        List<Game> List();
        void Create(int gameLength, string winningNumbers);
        void Update(Game entity);
    }
}