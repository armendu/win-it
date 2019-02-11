using System;
using System.Collections.Generic;
using System.Linq;
using Common.RepositoryInterfaces;
using DataAccess.Database;
using Entities.Models;

namespace DataAccess.Repository
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly EntityContext _entityContext;

        public CitiesRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public List<City> List(string country)
        {
            try
            {
                List<City> cities = _entityContext.Cities.Where(c => c.Country == country).OrderBy(c => c.Name).ToList();

                if (cities.Count == 0)
                    throw new NullReferenceException();

                return cities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> ListCountries()
        {
            try
            {
                List<string> countries = _entityContext.Cities.GroupBy(c => c.Country).Select(x => x.FirstOrDefault().Country).ToList();

                if (countries.Count == 0)
                    throw new NullReferenceException();

                return countries;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}