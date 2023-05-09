using Geography.Models;

namespace Geography.Interfaces;

public interface IProvinceRepository
{
  ICollection<Province> GetProvinces();
  Province GetProvince(int id);
  bool isProvinceExist(int id);
}