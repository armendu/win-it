using System.Collections.Generic;
using Entities.Models;

namespace Common.RepositoryInterfaces
{
    public interface ICitiesRepository
    {
        List<City> List(string country);

        List<string> ListCountries();
    }
}