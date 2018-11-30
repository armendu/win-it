using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(int id);
        List<T> List<T>();
        T Add<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);
    }
}