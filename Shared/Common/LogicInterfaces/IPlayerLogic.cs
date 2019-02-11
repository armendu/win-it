using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface IPlayerLogic
    {
        Player GetById(int gameId);
        void Update(int id);
    }
}