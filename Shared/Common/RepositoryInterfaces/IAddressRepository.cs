using System.Collections.Generic;
using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface IAddressRepository
    {
        Address GetById(int id);
        List<Address> List();
        void Update(Address entity);
        void Delete(Address entity);
    }
}