using Geography.Data;
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
}