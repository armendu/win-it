using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers.Exceptions;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;

namespace DataAccess.Repository
{
    public class AddressRepository: IAddressRepository
    {
        private readonly EntityContext _entityContext;

        public AddressRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public Address GetById(int id)
        {
            try
            {
                Address address = _entityContext.Addresses.FirstOrDefault(a => a.AddressId == id);

                if (address == null)
                    throw new NotFoundException($"Address with Id: {id} was not found!");

                return address;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Address> List()
        {
            try
            {
                List<Address> addresses = _entityContext.Addresses.ToList();

                if (addresses.Count == 0)
                    throw new NotFoundException("There are addresses to be shown!");

                return addresses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Address entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Address entity)
        {
            throw new System.NotImplementedException();
        }
    }
}