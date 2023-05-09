using Geography.Models;

namespace Geography.Interfaces;

public interface IProvinceRepository
{
  ICollection<Province> GetProvinces();
  ICollection<Province> GetProvincesByCountry(int id);
  Province GetProvince(int id);
  bool isProvinceExist(int id);
}