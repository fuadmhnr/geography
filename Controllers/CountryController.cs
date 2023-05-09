using Microsoft.AspNetCore.Mvc;
using Geography.Interfaces;
using Geography.Models;

namespace Geography.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
  private readonly ICountryRepository _countryRepository;

  public CountryController(ICountryRepository countryRepository)
  {
    _countryRepository = countryRepository;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
  public IActionResult GetCountries()
  {
    var countries = _countryRepository.GetCountries();
    
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