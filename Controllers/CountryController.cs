using Microsoft.AspNetCore.Mvc;
using Geography.Interfaces;
using Geography.Models;
using AutoMapper;
using Geography.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Geography.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
  private readonly ICountryRepository _countryRepository;
  private readonly IProvinceRepository _provinceRepository;
  private readonly IMapper _mapper;

  public CountryController(ICountryRepository countryRepository, IMapper mapper, IProvinceRepository provinceRepository)
  {
    _countryRepository = countryRepository;
    _mapper = mapper;
    _provinceRepository = provinceRepository;
  }

  [HttpGet, Authorize(Roles = "admin")]
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

  [HttpPost]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  public IActionResult CreateCountry([FromBody] CountryRequestDto requestDto)
  {
    if(requestDto is null)
    {
      return BadRequest(ModelState);
    }

    var existingCountry = _countryRepository.GetCountries().Where(c => c.name.Trim().ToUpper() == requestDto.name.Trim().ToUpper()).FirstOrDefault();

    if(existingCountry != null)
    {
      ModelState.AddModelError("", "Country already exist");
      return StatusCode(422, ModelState);
    }

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var countryMap = _mapper.Map<Country>(requestDto);

    if(!_countryRepository.CreateCountry(countryMap))
    {
      ModelState.AddModelError("", "Something went wrong while saving!");
      return StatusCode(500, ModelState);
    }

    return Ok("Country successfully created");
  }

  [HttpPut("{id}")]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public IActionResult UpdateCountry(int id, [FromBody] CountryRequestDto requestDto)
  {
    if(requestDto == null)
    {
      return BadRequest(ModelState);
    }

    if(!_countryRepository.isCountryExist(id))
    {
      return NotFound();
    }

    if(!ModelState.IsValid)
    {
      return BadRequest();
    }

    var countryToUpdate = _countryRepository.GetCountry(id);

    _mapper.Map(requestDto, countryToUpdate);
    
    if(!_countryRepository.UpdateCountry(countryToUpdate))
    {
      ModelState.AddModelError("payload", "Something when wrong while updating");
      return StatusCode(500, ModelState);
    }

    return NoContent();
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public IActionResult DeleteCountry(int id)
  {
    if(!_countryRepository.isCountryExist(id))
    {
      return NotFound();
    }

    var countryToDelete = _countryRepository.GetCountry(id);

    if(_provinceRepository.GetProvincesByCountry(id).Count > 0)
    {
      ModelState.AddModelError("", "This country have related province data");
      return StatusCode(422, ModelState);
    }

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if(!_countryRepository.DeleteCountry(countryToDelete))
    {
      ModelState.AddModelError("", "Something went wrong deleting country");
    }

    return NoContent();
  }
}