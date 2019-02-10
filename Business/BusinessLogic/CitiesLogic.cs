using System.Collections.Generic;
using Common.LogicInterfaces;
using Common.RepositoryInterfaces;
using Entities.Models;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class CitiesLogic: ICitiesLogic
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesLogic(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public List<City> List(string country)
        {
            try
            {
                List<City> cities = _citiesRepository.List(country);
                return cities;
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public List<string> ListCountries()
        {
            try
            {
                List<string> countries = _citiesRepository.ListCountries();
                return countries;
            }
            catch (MySqlException)
            {
                throw;
            }
        }
    }
}