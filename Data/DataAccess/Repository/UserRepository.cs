using System.Collections.Generic;
using Common.Interfaces;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public class UserRepository: IRepository
    {
        private readonly EntityContext _entityContext;

        public UserRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public T GetById<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<T> List<T>()
        {
            throw new System.NotImplementedException();
        }

        public T Add<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}