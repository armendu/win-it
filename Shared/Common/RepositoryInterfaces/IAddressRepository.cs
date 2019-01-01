using System.Collections.Generic;
using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface IAddressRepository
    {
        Address GetById(int id);
        List<Address> List();
        Address Create(Address entity);
        void Update(Address entity);
        void Delete(Address entity);
    }
}