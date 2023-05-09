using Geography.Models;

namespace Geography.Interfaces;

public interface ICountryRepository
{
  ICollection<Country> GetCountries();
  Country GetCountry(int id);
  bool isCountryExist(int id);
}