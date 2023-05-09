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
}