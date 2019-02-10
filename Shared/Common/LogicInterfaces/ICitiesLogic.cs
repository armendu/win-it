using System.Collections.Generic;
using Entities.Models;

namespace Common.LogicInterfaces
{
    public interface ICitiesLogic
    {
        List<City> List(string country);

        List<string> ListCountries();
    }
}