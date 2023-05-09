using Geography.Dto;
using Geography.Models;

namespace Geography.Interfaces;

public interface ICountryRepository
{
  ICollection<Country> GetCountries();
  Country GetCountry(int id);
  bool isCountryExist(int id);
  bool CreateCountry(Country country);
  bool UpdateCountry(Country country);
  bool Save();
}