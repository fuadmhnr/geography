using AutoMapper;
using Geography.Dto;
using Geography.Models;

namespace Geography.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Province, ProvinceDto>();
    CreateMap<Province, ProvinceRequestDto>();
    CreateMap<ProvinceRequestDto, Province>();
    CreateMap<Country, CountryDto>();
    CreateMap<Country, CountryRequestDto>();
    CreateMap<CountryRequestDto, Country>();
  }
}