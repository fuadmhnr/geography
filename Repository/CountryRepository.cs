using Geography.Data;
using Geography.Dto;
using Geography.Interfaces;
using Geography.Models;
using Microsoft.EntityFrameworkCore;

namespace Geography.Repository;

public class CountryRepository : ICountryRepository
{
  private readonly DataContext _context;

  public CountryRepository(DataContext context)
  {
    _context = context;
  }

  public bool CreateCountry(Country country)
  {
    _context.Add(country);
    return Save();
  }

  public ICollection<Country> GetCountries()
  {
    return _context.Countries.ToList();
  }

  public Country GetCountry(int id)
  {
    return _context.Countries.Where(c => c.id == id).Include(p => p.provinces).FirstOrDefault();
  }

  public bool isCountryExist(int id)
  {
    return _context.Countries.Any(c => c.id == id);
  }

  public bool Save()
  {
    var saved = _context.SaveChanges();
    return saved > 0;
  }
}