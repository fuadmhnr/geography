using Geography.Models;

namespace Geography.Interfaces;

public interface IProvinceRepository
{
  ICollection<Province> GetProvinces();
  ICollection<Province> GetProvincesByCountry(int id);
  Province GetProvince(int id);
  bool CreateProvince(Province province);
  bool UpdateProvince(Province province);
  bool DeleteProvince(Province province);
  bool isProvinceExist(int id);
  bool Save();
}