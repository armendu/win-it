using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface IPlayerRepository
    {
        Player GetById(int id);

        void Update(int id);
    }
}