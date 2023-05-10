using System.Security.Claims;
using Geography.Interfaces;

namespace Geography.Repository;

public class UserRepository : IUserRepository
{
  private readonly IHttpContextAccessor _httpContextAssessor;

  public UserRepository(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAssessor = httpContextAccessor;
  }

  public string GetMe()
  {
    var result = string.Empty;
    
    if(_httpContextAssessor.HttpContext != null)
    {
      result = _httpContextAssessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }

    return result;
  }
}