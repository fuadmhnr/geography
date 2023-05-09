using Microsoft.AspNetCore.Mvc;
using Geography.Interfaces;
using Geography.Models;
using AutoMapper;
using Geography.Dto;

namespace Geography.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
  private readonly ICountryRepository _countryRepository;
  private readonly IMapper _mapper;

  public CountryController(ICountryRepository countryRepository, IMapper mapper)
  {
    _countryRepository = countryRepository;
    _mapper = mapper;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
  public IActionResult GetCountries()
  {
    var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(countries);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(200, Type = typeof(Country))]
  [ProducesResponseType(400)]
  public IActionResult GetCountry(int id)
  {
    if(!_countryRepository.isCountryExist(id))
    {
      return NotFound();
    }

    var country = _countryRepository.GetCountry(id);

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(country);
  }

}