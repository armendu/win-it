using System.Collections.Generic;
using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface IGameRepository
    {
        Game GetById(string id);
        List<Game> List();
        Game Create(Game entity);
        void Update(Game entity);
        void Delete(Game entity);
    }
}