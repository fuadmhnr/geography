using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Geography.Data;
using Geography.Dto;
using Geography.Interfaces;
using Geography.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Geography.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
  public static User user = new User();
  private readonly DataContext _context;
  private readonly IConfiguration _configuration;
  private readonly IUserRepository _userRepository;

  public AuthController(DataContext context, IConfiguration configuration, IUserRepository userRepository)
  {
    _context = context;
    _configuration = configuration;
    _userRepository = userRepository;
  }


  [HttpGet("get-me"), Authorize]
  public IActionResult GetMe()
  {
    // // var username = User?.Identity?.Name;
    // var username = User.FindFirstValue(ClaimTypes.Name);
    // var role = User.FindFirstValue(ClaimTypes.Role);
    // return Ok(new {username, role});

    var username = _userRepository.GetMe();
    return Ok(username);
  }


  [HttpPost("register")]
  public IActionResult Register(UserDto request)
  {
    CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

    var user = new User()
    {
      username = request.username,
      password_salt = passwordSalt,
      password_hash = passwordHash,
    };

    _context.Users.Add(user);
    _context.SaveChanges();

    return Ok(user);
  }

  [HttpPost("login")]
  public IActionResult Login(UserDto request)
  {
    var isUserExist = _context.Users.Any(u => u.username == request.username);

    if(!isUserExist)
    {
      return NotFound();
    }

    var user = _context.Users.Where(u => u.username == request.username).SingleOrDefault();

    if(!VerifyPasswordHash(request.password, user.password_hash, user.password_salt))
    {
      ModelState.AddModelError("payload", "Credentials is not match");
      return StatusCode(401, ModelState);
    }

    string token = CreateToken(user);

    return Ok(new {token});
  }

  private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
  {
    using(var hmac = new HMACSHA512())
    {
      passwordSalt = hmac.Key;
      passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
  }

  private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
  {
    using (var hmac = new HMACSHA512(passwordSalt))
    {
      var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      return computedHash.SequenceEqual(passwordHash);
    }
  }

  private string CreateToken(User user)
  {
    List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "admin")
            };

    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        _configuration.GetSection("AppSettings:Token").Value));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: creds);

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    return jwt;
  }
}