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
  private readonly IProvinceRepository _ProvinceRepository;
  private readonly IMapper _mapper;

  public ProvinceController(IProvinceRepository ProvinceRepository, IMapper mapper)
  {
    _ProvinceRepository = ProvinceRepository;
    _mapper = mapper;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Province>))]
  public IActionResult GetProvinces()
  {
    var provinces = _mapper.Map<List<ProvinceDto>>(_ProvinceRepository.GetProvinces());

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
    if(!_ProvinceRepository.isProvinceExist(id))
    {
      return NotFound();
    }

    var Province = _ProvinceRepository.GetProvince(id);

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(Province);
  }

}