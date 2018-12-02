using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> List();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}