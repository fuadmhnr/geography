using Geography.Data;
using Geography.Interfaces;
using Geography.Models;
using Microsoft.EntityFrameworkCore;

namespace Geography.Repository;

public class ProvinceRepository : IProvinceRepository
{
  private readonly DataContext _context;
  
  public ProvinceRepository(DataContext context)
  {
    _context = context;
  }
  public ICollection<Province> GetProvinces()
  {
    return _context.Provinces.ToList();
  }

  public Province GetProvince(int id)
  {
    return _context.Provinces.Where(p => p.id == id).Include(e => e.country).SingleOrDefault();
  }

  public bool isProvinceExist(int id)
  {
    return _context.Provinces.Any(p => p.id == id);
  }

  public ICollection<Province> GetProvincesByCountry(int id)
  {
    return _context.Provinces.Where(p => p.country_id == id).ToList();
  }

  public bool CreateProvince(Province province)
  {
    _context.Add(province);
    return Save();
  }

  public bool UpdateProvince(Province province)
  {
    _context.Update(province);
    return Save();
  }

  public bool DeleteProvince(Province province)
  {
    _context.Remove(province);
    return Save();
  }

  public bool Save()
  {
    var saved = _context.SaveChanges();
    return saved > 0;
  }
}