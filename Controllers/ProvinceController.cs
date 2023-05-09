using Microsoft.AspNetCore.Mvc;
using Geography.Interfaces;
using Geography.Models;
using AutoMapper;
using Geography.Dto;

namespace Geography.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProvinceController : Controller
{
  private readonly IProvinceRepository _provinceRepository;
  private readonly IMapper _mapper;

  public ProvinceController(IProvinceRepository provinceRepository, IMapper mapper)
  {
    _provinceRepository = provinceRepository;
    _mapper = mapper;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Province>))]
  public IActionResult GetProvinces()
  {
    var provinces = _mapper.Map<List<ProvinceDto>>(_provinceRepository.GetProvinces());

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(provinces);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(200, Type = typeof(Province))]
  [ProducesResponseType(400)]
  public IActionResult GetProvince(int id)
  {
    if(!_provinceRepository.isProvinceExist(id))
    {
      return NotFound();
    }

    var Province = _provinceRepository.GetProvince(id);

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(Province);
  }

  [HttpPost]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  public IActionResult CreateProvince([FromBody] ProvinceRequestDto requestDto)
  {
    if (requestDto is null)
    {
      return BadRequest(ModelState);
    }

    var existingProvince = _provinceRepository.GetProvinces().Where(p => p.name.Trim().ToUpper() == requestDto.name.Trim().ToUpper() && p.country_id == requestDto.country_id).SingleOrDefault();

    if(existingProvince != null)
    {
      ModelState.AddModelError("payload", "Province already exists");
      return StatusCode(422, ModelState);
    }

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var provinceMap = _mapper.Map<Province>(requestDto);

    if(!_provinceRepository.CreateProvince(provinceMap))
    {
      ModelState.AddModelError("", "Something went wrong while saving");
      return StatusCode(500, ModelState);
    }

    return Ok("Province successfully created");
  }

  [HttpPut("{id}")]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public IActionResult UpdateCountry(int id, [FromBody] ProvinceRequestDto requestDto)
  {
    if(requestDto is null)
    {
      return BadRequest(ModelState);
    }

    if(!_provinceRepository.isProvinceExist(id))
    {
      return NotFound();
    }

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var provinceToUpdate = _provinceRepository.GetProvince(id);

    _mapper.Map(requestDto, provinceToUpdate);

    if(!_provinceRepository.UpdateProvince(provinceToUpdate))
    {
      ModelState.AddModelError("payload", "Something went wrong while updating");
      return StatusCode(500, ModelState);
    }

    return NoContent();
  }

}